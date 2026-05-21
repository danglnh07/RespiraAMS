using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RespiraAMS.Core.Dtos;
using RespiraAMS.Core.Exceptions;
using RespiraAMS.Core.Models;
using RespiraAMS.Core.RepositoryContract;
using RespiraAMS.Core.ServiceContract;
using RespiraAMS.Infrastructure.Profiles;

namespace RespiraAMS.Infrastructure.Services;

public class DiseaseService(IUnitOfWork uow, DiseaseProfile profile, ILogger<DiseaseService> logger) : IDiseaseService
{
    private static Func<IQueryable<Disease>, IQueryable<Disease>> QueryFullData()
    {
        return query => query
            .AsSplitQuery() // Performance for multiple include
            .Include(x => x.IcuHospitalizeCriteria)
            .ThenInclude(x => x.Criterion)
            .Include(x => x.ResistanceRisks)
            .ThenInclude(x => x.Criterion)
            .Include(x => x.ResistanceRisks)
            .ThenInclude(x => x.Pathogen)
            .Include(x => x.DiseasePathogens)
            .ThenInclude(x => x.Pathogen);
    }

    public async Task<Guid> CreateAsync(DiseaseDtoRequest req)
    {
        // Map from request to model
        var disease = profile.ToModel(req);

        // Save to database
        await uow.Repo<Disease>().CreateAsync(disease);
        
        // Return ID
        if (await uow.SaveChangesAsync() > 0) return disease.Id;
        
        // Throw error if failed to save changes
        logger.LogWarning("Failed to create disease");
        throw new InternalServerErrorException();
    }

    public async Task<DiseaseDtoResponse> GetByIdAsync(Guid id)
    {
        var disease = await uow.Repo<Disease>().GetByIdAsync(id, QueryFullData());
        return disease is null ? throw new NotFoundException(nameof(Disease), id) : profile.ToResp(disease);
    }

    public async Task<(PaginationMetadata, IEnumerable<DiseaseDtoResponse>)> GetListAsync(int page, int size)
    {
        var diseases = await uow.Repo<Disease>().GetPagedListAsync(page, size, QueryFullData());
        return profile.ToPagedResponse(diseases);
    }

    public async Task<DiseaseDtoResponse> UpdateAsync(Guid id, DiseaseDtoRequest req)
    {
        // Get disease by ID
        var disease = await uow.Repo<Disease>().GetByIdAsync(id);
        if (disease is null)
        {
            throw new NotFoundException(nameof(Disease), id);
        }

        // Map from request to model
        disease = profile.MapModel(req, disease);

        // Save database
        uow.Repo<Disease>().Update(disease);
        
        // Refetch data
        if (await uow.SaveChangesAsync() > 0) return await GetByIdAsync(id);
        
        // Throw error if failed to save changes
        logger.LogWarning("Failed to update disease");
        throw new InternalServerErrorException();
    }

    public async Task DeleteAsync(Guid id)
    {
        // Get disease by ID
        var disease = await uow.Repo<Disease>().GetByIdAsync(id, query => query
            .AsSplitQuery() // Performance for multiple include
            .Include(x => x.ResistanceRisks)
            .Include(x => x.DiseasePathogens)
            .Include(x => x.IcuHospitalizeCriteria)
            .Include(x => x.TreatmentProtocols));
        if (disease is null)
        {
            throw new NotFoundException(nameof(Disease), id);
        }

        // Delete cascade in transaction
        await uow.ExecuteInTransactionAsync(() =>
        {
            // Delete disease
            uow.Repo<Disease>().SoftDelete(disease);

            // Cascade delete
            logger.LogInformation("Start cascade delete resistance risk factor: {count}", disease.ResistanceRisks.Count);
            foreach (var risk in disease.ResistanceRisks)
            {
                uow.Repo<ResistanceRiskFactor>().SoftDelete(risk);
            }
        
            logger.LogInformation("Start cascade delete disease pathogens: {count}", disease.DiseasePathogens.Count);
            foreach (var diseasePathogens in disease.DiseasePathogens)
            {
                uow.Repo<DiseasePathogen>().SoftDelete(diseasePathogens);
            }

            logger.LogInformation("Start cascade delete ICU hospitalize criteria: {count}", disease.IcuHospitalizeCriteria.Count);
            foreach (var icu in disease.IcuHospitalizeCriteria)
            {
                uow.Repo<IcuHospitalizeCriterion>().SoftDelete(icu);
            }

            logger.LogInformation("Start cascade delete treatment protocols: {count}", disease.TreatmentProtocols.Count);
            foreach (var protocol in disease.TreatmentProtocols)
            {
                uow.Repo<TreatmentProtocol>().SoftDelete(protocol);
            }
        });
    }
}