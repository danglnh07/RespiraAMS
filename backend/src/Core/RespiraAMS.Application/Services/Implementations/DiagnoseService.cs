using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RespiraAMS.Application.Dtos;
using RespiraAMS.Application.Profiles.Implementations;
using RespiraAMS.Application.RepositoryContracts;
using RespiraAMS.Application.Services.Contracts;
using RespiraAMS.Domain.Enums;
using RespiraAMS.Domain.Exceptions;
using RespiraAMS.Domain.Models;

namespace RespiraAMS.Application.Services.Implementations;

public class DiagnoseService(
    IUnitOfWork uow,
    CriterionProfile criterionProfile,
    PathogenProfile pathogenProfile,
    TreatmentProtocolProfile treatmentProtocolProfile,
    ILogger<DiagnoseService> logger) : IDiagnoseService
{
    public static int CurbScore(bool confusion, double? urea, int respiratory, int systolic, int diastolic, int age)
    {
        var score = 0;
        if (confusion) score++;
        if (urea > 7) score++;
        if (respiratory >= 30) score++;
        if (systolic < 90 || diastolic < 60) score++;
        if (age >= 65) score++;
        return score;
    }

    public static (Severity, TreatmentSite) Curb65(int score)
    {
        return score switch
        {
            >= 0 and <= 1 => (Severity.Mild, TreatmentSite.Outpatient),
            2 => (Severity.Moderate, TreatmentSite.Inpatient),
            >= 3 and <= 5 => (Severity.Severe, TreatmentSite.IntensiveCareUnit),
            _ => throw new UnexpectedException($"CURB65 score get an unexpected value: {score}")
        };
    }

    public static (Severity, TreatmentSite) Crb65(int score)
    {
        return score switch
        {
            0 => (Severity.Mild, TreatmentSite.Outpatient),
            1 or 2 => (Severity.Moderate, TreatmentSite.Inpatient),
            3 or 4 => (Severity.Severe, TreatmentSite.IntensiveCareUnit),
            _ => throw new UnexpectedException($"CRB65 score get an unexpected value: {score}")
        };
    }

    public async Task<DiagnosisTemplateDto> GetDiagnosisTemplate(Guid diseaseId)
    {
        var sw = Stopwatch.StartNew();

        // Get disease including all the criteria
        var disease = await uow.Repo<Disease>().GetByIdAsync(diseaseId, query => query
            .AsSplitQuery()
            .Include(x => x.IcuHospitalizeCriteria)
            .ThenInclude(x => x.Criterion)
            .Include(x => x.ResistanceRisks)
            .ThenInclude(x => x.Criterion));

        if (disease is null)
        {
            logger.LogWarning("Disease ID not found: {DiseaseID}", diseaseId);
            throw new BadRequestException("Disease not found");
        }

        // Get other criteria from treatment protocol
        var protocols = await ((ITreatmentProtocolRepository)uow.Repo<TreatmentProtocol>())
            .GetTreatmentProtocolsAsync(query => query
                .AsSplitQuery()
                .Where(x => x.DiseaseId == diseaseId)
                .Include(x => x.OtherCriteria));

        // Since TreatmentProtocol.OtherCriteria can reference to the same criteria as ICU or ResistanceRisk,
        // we need to deduplicate the IDs by using a hash set
        var existingIds = disease.IcuHospitalizeCriteria
            .Select(x => x.Criterion.Id)
            .Concat(disease.ResistanceRisks.Select(x => x.CriterionId))
            .ToHashSet();

        // Return diagnose template
        var result = new DiagnosisTemplateDto
        {
            IcuHospitalizeCriteria = disease.IcuHospitalizeCriteria
                .Select(x => criterionProfile.ToResp(x.Criterion))
                .ToList(),
            ResistanceRiskFactors = disease.ResistanceRisks
                .Select(x => criterionProfile.ToResp(x.Criterion))
                .ToList(),
            OtherCriteria = protocols
                .SelectMany(p => p.OtherCriteria) // Flattens the nested lists
                .Where(c => !existingIds.Contains(c.Id))
                .DistinctBy(c => c.Id) // Prevents duplicates if multiple protocols share the same criterion
                .Select(criterionProfile.ToResp)
                .ToList()
        };

        sw.Stop();
        logger.LogInformation("Prepare diagnose template complete: {result}", new
        {
            IcuCriteriaCount = result.IcuHospitalizeCriteria.Count,
            RiskFactorCount = result.ResistanceRiskFactors.Count,
            OtherCriteriaCount = result.OtherCriteria.Count,
            ExecutionTimeInMs = sw.ElapsedMilliseconds
        });

        return result;
    }

    public async Task<DiagnosisResultDto> Diagnose(Guid diseaseId, ClinicalPictureDto clinicalPicture)
    {
        // Get disease
        var disease = await uow.Repo<Disease>().GetByIdAsync(diseaseId, query => query
            .AsSplitQuery()
            .Include(x => x.IcuHospitalizeCriteria)
            .ThenInclude(x => x.Criterion)
            .Include(x => x.ResistanceRisks)
            .ThenInclude(x => x.Criterion)
            .Include(x => x.ResistanceRisks)
            .ThenInclude(x => x.Pathogen));
        if (disease is null)
        {
            logger.LogWarning("Disease ID not found: {DiseaseID}", diseaseId);
            throw new BadRequestException("Disease not found");
        }

        // Assess severity and treatment site using CURB65 score
        var curbScore = CurbScore(clinicalPicture.Confusion, clinicalPicture.Urea, clinicalPicture.Respiratory,
            clinicalPicture.Systolic, clinicalPicture.Diastolic, clinicalPicture.Age);
        var (severity, treatmentSite) = clinicalPicture.Urea is null ? Crb65(curbScore) : Curb65(curbScore);

        // Assess whether patient need to go to ICU using AST metrics
        var (mainScore, secondaryScore) = (0, 0);
        foreach (var option in clinicalPicture.IcuHospitalizeCriteria)
        {
            var criterion = disease.IcuHospitalizeCriteria
                .FirstOrDefault(x => x.CriterionId == option);
            if (criterion is null)
            {
                logger.LogWarning("Clinical picture ICU hospitalize criterion not found: {CriterionId}", option);
                throw new BadRequestException("Clinical picture ICU hospitalize criterion not found");
            }

            if (criterion.IsMainCriteria)
            {
                mainScore++;
            }
            else
            {
                secondaryScore++;
            }
        }

        // Prioritize AST metric over CURB65 for treatment site
        logger.LogInformation("AST metrics: {main} - {secondary}", mainScore, secondaryScore);
        if (disease.RequiredIcuMainCriteria >= mainScore || disease.RequiredIcuSecondaryCriteria >= secondaryScore)
        {
            treatmentSite = TreatmentSite.IntensiveCareUnit;
        }

        // Assess special infection probability
        var scores = new Dictionary<Guid, int>();
        foreach (var option in clinicalPicture.ResistanceRiskFactors)
        {
            var factor = disease.ResistanceRisks
                .FirstOrDefault(x => x.CriterionId == option);
            if (factor is null)
            {
                logger.LogWarning("Resistance risk factor not found: {CriterionId}", option);
                throw new BadRequestException("Resistance risk factor not found");
            }

            if (scores.TryGetValue(factor.PathogenId, out _))
            {
                scores[factor.PathogenId]++;
            }
            else
            {
                scores.Add(factor.PathogenId, 1);
            }
        }

        var probabilities = new List<InfectionProbabilityDto>();
        var factorsByPathogen = disease.ResistanceRisks.GroupBy(x => x.Pathogen);
        foreach (var factor in factorsByPathogen)
        {
            var key = factor.Key;
            if (!scores.TryGetValue(factor.Key.Id, out _)) continue;
            var value = (double)scores[factor.Key.Id] / factor.Count();
            probabilities.Add(new InfectionProbabilityDto
            {
                Pathogen = pathogenProfile.ToResp(key),
                Probability = value,
            });
        }

        return new DiagnosisResultDto
        {
            Severity = severity,
            TreatmentSite = treatmentSite,
            InfectionProbabilities = probabilities,
        };
    }

    public async Task<IEnumerable<TreatmentProtocolDtoResponse>> Recommend(Guid diseaseId, RecommendDtoRequest req)
    {
        var sw = Stopwatch.StartNew();

        // Query the treatment protocols
        var protocols = await ((ITreatmentProtocolRepository)uow.Repo<TreatmentProtocol>())
            .GetTreatmentProtocolsAsync(query => query
                .AsSplitQuery()
                .Include(x => x.SpecialInfection)
                .Include(x => x.Medicines)
                .ThenInclude(x => x.AntibioticSpectrum)
                .Include(x => x.OtherCriteria)
                .Where(x => x.DiseaseId == diseaseId && x.Severity == req.Severity &&
                            x.TreatmentSite == req.TreatmentSite));

        // Sort the protocols by the probability infection and the number of criteria match
        // Sort by relevance:
        // 1. Higher special-infection probability match → higher priority
        // 2. More matching other criteria → higher priority
        var sorted = protocols
            .OrderByDescending(p =>
                p.SpecialInfectionId is not null
                    ? req.InfectionProbabilities.GetValueOrDefault(p.SpecialInfectionId.Value, 0d)
                    : 0d)
            .ThenByDescending(p => p.OtherCriteria.Count(c => req.OtherCriteria.Contains(c.Id)))
            .ToList();

        logger.LogInformation("Recommend treatment protocol success: {result}", new
        {
            Total = sorted.Count,
            ExecutionTimeInMs = sw.ElapsedMilliseconds,
        });
        return sorted.Select(treatmentProtocolProfile.ToResp);
    }
}