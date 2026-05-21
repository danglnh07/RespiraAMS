using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RespiraAMS.Application.Dtos;
using RespiraAMS.Application.Profiles.Implementations;
using RespiraAMS.Application.Services.Contracts;
using RespiraAMS.Domain.Exceptions;
using RespiraAMS.Domain.Models;
using RespiraAMS.Domain.RepositoryContracts;

namespace RespiraAMS.Application.Services.Implementations;

public class ResistanceRiskFactorService(
    IUnitOfWork uow,
    ResistanceRiskFactorProfile profile,
    ILogger<ResistanceRiskFactorService> logger) : IResistanceRiskFactorService
{
    private async Task AreFkExisted(Guid diseaseId, Guid pathogenId)
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

    private static Func<IQueryable<ResistanceRiskFactor>, IQueryable<ResistanceRiskFactor>> QueryFullData()
    {
        return query => query
            .AsSplitQuery() // Performance for multiple include
            .Include(x => x.Criterion)
            .Include(x => x.Pathogen);
    }

    public async Task<Guid> CreateAsync(ResistanceRiskFactorDtoRequest req)
    {
        // Check if the associate IDs (foreign keys) are existed
        await AreFkExisted(req.DiseaseId, req.PathogenId);

        // Map from request to model
        var criterion = profile.ToModel(req);

        // Save changes to database
        await uow.Repo<ResistanceRiskFactor>().CreateAsync(criterion);
        
        // Return ID
        if (await uow.SaveChangesAsync() > 0) return criterion.Id;
        
        // Throw error if failed to save changes to database
        logger.LogWarning("Failed to create resistance risk factor");
        throw new InternalServerErrorException();

    }

    public async Task<ResistanceRiskFactorDtoResponse> GetByIdAsync(Guid id)
    {
        var criterion = await uow.Repo<ResistanceRiskFactor>().GetByIdAsync(id, QueryFullData());
        return criterion is null ? throw new NotFoundException(nameof(ResistanceRiskFactor), id) : profile.ToResp(criterion);
    }

    public async Task<(PaginationMetadata, IEnumerable<ResistanceRiskFactorDtoResponse>)> GetListAsync(int page, int size)
    {
        var criteria = await uow.Repo<ResistanceRiskFactor>().GetPagedListAsync(page, size, QueryFullData());
        return profile.ToPagedResponse(criteria);
    }

    public async Task<ResistanceRiskFactorDtoResponse> UpdateAsync(Guid id, ResistanceRiskFactorDtoRequest req)
    {
        // Get criterion by ID
        var criterion = await uow.Repo<ResistanceRiskFactor>().GetByIdAsync(id, QueryFullData());
        if (criterion is null)
        {
            throw new NotFoundException(nameof(ResistanceRiskFactor), id);
        }
        
        // Check if the associate IDs (foreign keys) are existed
        await AreFkExisted(req.DiseaseId, req.PathogenId);

        // Map request to model
        criterion = profile.MapModel(req, criterion);

        // Save to database
        uow.Repo<ResistanceRiskFactor>().Update(criterion);
        
        // Return new updated data
        if (await uow.SaveChangesAsync() > 0) return await GetByIdAsync(id);
        
        // Throw error if failed to save changes to database
        logger.LogWarning("Failed to update resistance risk factor");
        throw new InternalServerErrorException();
    }

    public async Task DeleteAsync(Guid id)
    {
        /*
         * Same case with IcuHospitalizeCriteria, the criterion object can be reference by treatment protocol
         * so we won't cascade delete it
         */
        
        // Get criterion by ID
        var criterion = await uow.Repo<ResistanceRiskFactor>().GetByIdAsync(id);
        if (criterion is null)
        {
            throw new NotFoundException(nameof(ResistanceRiskFactor), id);
        }

        // Delete criterion
        uow.Repo<ResistanceRiskFactor>().SoftDelete(criterion);
        if (await uow.SaveChangesAsync() <= 0)
        {
            logger.LogWarning("Failed to delete resistance risk factor");
            throw new InternalServerErrorException();
        }
    }
}