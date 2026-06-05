using BuildingBlocks.Exceptions;
using Microsoft.EntityFrameworkCore;
using RespiraAMS.Application.Abstracts.CQRS;
using RespiraAMS.Application.Abstracts.Data;
using RespiraAMS.Application.Shared.Dtos;
using RespiraAMS.Domain.Enums;
using RespiraAMS.Domain.Models;

namespace RespiraAMS.Application.Features.Diseases.GetDiseaseById;

public class GetDiseaseByIdHandler(IDbContext context) : IQueryHandler<GetDiseaseByIdQuery, DiseaseResult>
{
    public async Task<DiseaseResult> HandleAsync(GetDiseaseByIdQuery query)
    {
        var disease = await context.Diseases
            .AsSplitQuery()
            .Where(x => x.Id == query.Id)
            .Select(x => new DiseaseResult()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                RequiredIcuMainCriteria = x.RequiredIcuMainCriteria,
                RequiredIcuSecondaryCriteria = x.RequiredIcuSecondaryCriteria,
                DiseasePathogens = x.DiseasePathogens.Select(y => new DiseasePathogenItem()
                {
                    Id = y.Id,
                    Severity = y.Severity,
                    TreatmentSite = y.TreatmentSite,
                    Pathogen = y.Pathogen.Name,
                }).ToList(),
                IcuHospitalizeCriteria = x.IcuHospitalizeCriteria.Select(y => new IcuHospitalizeCriterionItem()
                {
                    Id = y.Id,
                    IsMainCriteria = y.IsMainCriteria,
                    Criterion = new CriterionItem()
                    {
                        Id = y.Criterion.Id,
                        Name = y.Criterion.Name,
                        Type = y.Criterion.Type,
                        Min = y.Criterion.Type == CriterionType.Numeric ? ((NumericCriterion)y.Criterion).Min : null,
                        Max = y.Criterion.Type == CriterionType.Numeric ? ((NumericCriterion)y.Criterion).Max : null,
                        Unit = y.Criterion.Type == CriterionType.Numeric ? ((NumericCriterion)y.Criterion).Unit : null,
                        IsExclusive = y.Criterion.Type == CriterionType.Numeric
                            ? ((NumericCriterion)y.Criterion).IsExclusive
                            : null
                    }
                }).ToList(),
                ResistanceRisks = x.ResistanceRisks.Select(y => new ResistanceRiskFactorItem()
                {
                    Id = y.Id,
                    Name = y.Name,
                    Criterion = new CriterionItem()
                    {
                        Id = y.Criterion.Id,
                        Name = y.Criterion.Name,
                        Type = y.Criterion.Type,
                        Min = y.Criterion.Type == CriterionType.Numeric ? ((NumericCriterion)y.Criterion).Min : null,
                        Max = y.Criterion.Type == CriterionType.Numeric ? ((NumericCriterion)y.Criterion).Max : null,
                        Unit = y.Criterion.Type == CriterionType.Numeric ? ((NumericCriterion)y.Criterion).Unit : null,
                        IsExclusive = y.Criterion.Type == CriterionType.Numeric
                            ? ((NumericCriterion)y.Criterion).IsExclusive
                            : null
                    },
                    Pathogen = y.Pathogen.Name,
                }).ToList(),
                TreatmentProtocols = x.TreatmentProtocols.Select(y => new TreatmentProtocolItem()
                {
                    Id = y.Id,
                    Name = y.Name,
                    Issuer = y.Issuer,
                    IssueDate = y.IssueDate,
                    Version = y.Version,
                    LastUpdated = y.UpdatedAt,
                }).ToList(),
            })
            .FirstOrDefaultAsync();

        return disease ?? throw new NotFoundException(nameof(Disease), query.Id);
    }
}