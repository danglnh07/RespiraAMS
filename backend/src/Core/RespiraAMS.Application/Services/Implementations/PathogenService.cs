using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RespiraAMS.Application.Dtos;
using RespiraAMS.Application.Profiles.Implementations;
using RespiraAMS.Application.RepositoryContracts;
using RespiraAMS.Application.Services.Contracts;
using RespiraAMS.Domain.Exceptions;
using RespiraAMS.Domain.Models;

namespace RespiraAMS.Application.Services.Implementations;

public class PathogenService(IUnitOfWork uow, PathogenProfile profile, ILogger<PathogenService> logger)
    : IPathogenService
{
    public async Task<Guid> CreateAsync(PathogenDtoRequest req)
    {
        // Map from request DTO to model
        var pathogen = profile.ToModel(req);

        // Add pathogen to database
        await uow.Repo<Pathogen>().CreateAsync(pathogen);
        
        // Return ID
        if (await uow.SaveChangesAsync() > 0) return pathogen.Id;
        
        // Throw error if failed to save changes
        logger.LogWarning("Failed to create pathogen");
        throw new InternalServerErrorException();
    }

    public async Task<PathogenDtoResponse> GetByIdAsync(Guid id)
    {
        var pathogen = await uow.Repo<Pathogen>().GetByIdAsync(id);
        return pathogen is null ? throw new NotFoundException(nameof(Pathogen), id) : profile.ToResp(pathogen);
    }

    public async Task<(PaginationMetadata, IEnumerable<PathogenDtoResponse>)> GetListAsync(int page, int size)
    {
        var pathogens = await uow.Repo<Pathogen>().GetPagedListAsync(page, size);
        return profile.ToPagedResponse(pathogens);
    }

    public async Task<PathogenDtoResponse> UpdateAsync(Guid id, PathogenDtoRequest req)
    {
        // Get pathogen by ID
        var pathogen = await uow.Repo<Pathogen>().GetByIdAsync(id);
        if (pathogen is null)
        {
            throw new NotFoundException(nameof(Pathogen), id);
        }

        // Map request to model
        pathogen = profile.MapModel(req, pathogen);

        // Save changes to database
        uow.Repo<Pathogen>().Update(pathogen);
        
        // Pathogen only has scalar fields updated -> map to response
        if (await uow.SaveChangesAsync() > 0) return profile.ToResp(pathogen);
        
        // Throw error if failed to save changes
        logger.LogWarning("Failed to update pathogen");
        throw new InternalServerErrorException();
    }

    public async Task DeleteAsync(Guid id)
    {
        // Get pathogen by ID
        var pathogen = await uow.Repo<Pathogen>().GetByIdAsync(id, query => query
            .AsSplitQuery() // Performance for multiple include
            .Include(x => x.TreatmentProtocols)
            .Include(x => x.DiseasePathogens)
            .Include(x => x.ResistanceRiskFactors));
        if (pathogen is null)
        {
            throw new NotFoundException(nameof(Pathogen), id);
        }

        // Delete cascade in transaction
        await uow.ExecuteInTransactionAsync(() =>
        {
            // Delete pathogen
            uow.Repo<Pathogen>().SoftDelete(pathogen);

            // Cascade delete: ResistanceRiskFactor, DiseasePathogen, TreatmentProtocol
            logger.LogInformation("State cascade delete treatment protocols: {count}",
                pathogen.TreatmentProtocols.Count);
            foreach (var protocol in pathogen.TreatmentProtocols)
            {
                uow.Repo<TreatmentProtocol>().SoftDelete(protocol);
            }

            logger.LogInformation("State cascade delete disease pathogens: {count}", pathogen.DiseasePathogens.Count);
            foreach (var diseasePathogen in pathogen.DiseasePathogens)
            {
                uow.Repo<DiseasePathogen>().SoftDelete(diseasePathogen);
            }

            logger.LogInformation("State cascade delete resistance risk factors: {count}",
                pathogen.ResistanceRiskFactors.Count);
            foreach (var risk in pathogen.ResistanceRiskFactors)
            {
                uow.Repo<ResistanceRiskFactor>().SoftDelete(risk);
            }
        });
    }
}