using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RespiraAMS.Application.Dtos;
using RespiraAMS.Application.Profiles.Implementations;
using RespiraAMS.Application.Services.Contracts;
using RespiraAMS.Domain.Exceptions;
using RespiraAMS.Domain.Models;
using RespiraAMS.Domain.RepositoryContracts;

namespace RespiraAMS.Application.Services.Implementations;

public class DiseasePathogenService(
    IUnitOfWork uow,
    DiseasePathogenProfile profile,
    ILogger<DiseasePathogenService> logger) : IDiseasePathogenService
{
    private async Task AreFksExisted(Guid diseaseId, Guid pathogenId)
    {
        if (await uow.Repo<Disease>().GetByIdAsync(diseaseId) is null)
        {
            logger.LogWarning("Disease ID not exists: {DiseaseID}", diseaseId);
            throw new BadRequestException("Disease ID not exists");
        }

        if (await uow.Repo<Pathogen>().GetByIdAsync(pathogenId) is null)
        {
            logger.LogWarning("Pathogen ID not exists: {PathogenID}", pathogenId);
            throw new BadRequestException("Pathogen ID not exists");
        }
    }

    private static Func<IQueryable<DiseasePathogen>, IQueryable<DiseasePathogen>> QueryFullData()
    {
        return query => query.Include(x => x.Pathogen);
    }

    public async Task<Guid> CreateAsync(DiseasePathogenDtoRequest req)
    {
        // Check if associating IDs (foreign keys) exists
        await AreFksExisted(req.DiseaseId, req.PathogenId);

        // Map from request to model
        var pathogen = profile.ToModel(req);

        // Save to database
        await uow.Repo<DiseasePathogen>().CreateAsync(pathogen);
        
        // Return ID
        if (await uow.SaveChangesAsync() > 0) return pathogen.Id;
        
        // Throw error if failed to save changes
        logger.LogWarning("Failed to create disease pathogen");
        throw new InternalServerErrorException();

    }

    public async Task<DiseasePathogenDtoResponse> GetByIdAsync(Guid id)
    {
        var pathogen = await uow.Repo<DiseasePathogen>().GetByIdAsync(id, QueryFullData());
        return pathogen is null ? throw new NotFoundException(nameof(DiseasePathogen), id) : profile.ToResp(pathogen);
    }

    public async Task<(PaginationMetadata, IEnumerable<DiseasePathogenDtoResponse>)> GetListAsync(int page, int size)
    {
        var pathogens = await uow.Repo<DiseasePathogen>().GetPagedListAsync(page, size, QueryFullData());
        return profile.ToPagedResponse(pathogens);
    }

    public async Task<DiseasePathogenDtoResponse> UpdateAsync(Guid id, DiseasePathogenDtoRequest req)
    {
        // Get pathogen by ID
        var pathogen = await uow.Repo<DiseasePathogen>().GetByIdAsync(id, QueryFullData());
        if (pathogen is null)
        {
            throw new NotFoundException(nameof(DiseasePathogen), id);
        }

        // Check if associating IDs (foreign keys) exists
        await AreFksExisted(req.DiseaseId, req.PathogenId);

        // Map from request to model
        pathogen = profile.MapModel(req, pathogen);

        // Save to database
        uow.Repo<DiseasePathogen>().Update(pathogen);
        
        // Return new data after update
        if (await uow.SaveChangesAsync() > 0) return await GetByIdAsync(id);
        
        // Throw error if failed to save changes
        logger.LogWarning("Failed to update disease pathogen");
        throw new InternalServerErrorException();
    }

    public async Task DeleteAsync(Guid id)
    {
        // Get pathogen by ID
        var pathogen = await uow.Repo<DiseasePathogen>().GetByIdAsync(id);
        if (pathogen is null)
        {
            throw new NotFoundException(nameof(DiseasePathogen), id);
        }

        uow.Repo<DiseasePathogen>().SoftDelete(pathogen);
        if (await uow.SaveChangesAsync() <= 0)
        {
            logger.LogWarning("Failed to delete disease pathogen");
            throw new InternalServerErrorException();
        }
    }
}