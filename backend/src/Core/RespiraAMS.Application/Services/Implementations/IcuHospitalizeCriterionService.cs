using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RespiraAMS.Application.Dtos;
using RespiraAMS.Application.Profiles.Implementations;
using RespiraAMS.Application.Services.Contracts;
using RespiraAMS.Domain.Exceptions;
using RespiraAMS.Domain.Models;
using RespiraAMS.Domain.RepositoryContracts;

namespace RespiraAMS.Application.Services.Implementations;

public class IcuHospitalizeCriterionService(
    IUnitOfWork uow,
    IcuHospitalizeCriterionProfile profile,
    ILogger<IcuHospitalizeCriterionService> logger) : IIcuHospitalizeCriterionService
{
    private async Task IsFkExisted(Guid id)
    {
        if (await uow.Repo<Disease>().GetByIdAsync(id) is null)
        {
            logger.LogWarning("Disease ID not found: {Id}", id);
            throw new BadRequestException("Disease ID not exist");
        }
    }

    private static Func<IQueryable<IcuHospitalizeCriterion>, IQueryable<IcuHospitalizeCriterion>> QueryFullData()
    {
        return query => query.Include(x => x.Criterion);
    }

    public async Task<Guid> CreateAsync(IcuHospitalizedCriterionDtoRequest req)
    {
        // Check if disease exists
        await IsFkExisted(req.DiseaseId);

        // Map from request to model
        var icu = profile.ToModel(req);

        // Save to database
        await uow.Repo<IcuHospitalizeCriterion>().CreateAsync(icu);
        
        // Return ID
        if (await uow.SaveChangesAsync() > 0) return icu.Id;
        
        // Throw error if failed to save changes
        logger.LogWarning("Failed to create ICU hospitalize criterion");
        throw new InternalServerErrorException();
    }

    public async Task<IcuHospitalizedCriterionDtoResponse> GetByIdAsync(Guid id)
    {
        var criterion = await uow.Repo<IcuHospitalizeCriterion>().GetByIdAsync(id, QueryFullData());
        return criterion is null
            ? throw new NotFoundException(nameof(IcuHospitalizeCriterion), id)
            : profile.ToResp(criterion);
    }

    public async Task<(PaginationMetadata, IEnumerable<IcuHospitalizedCriterionDtoResponse>)> GetListAsync(int page, int size)
    {
        var criteria = await uow.Repo<IcuHospitalizeCriterion>()
            .GetPagedListAsync(page, size, QueryFullData());
        return profile.ToPagedResponse(criteria);
    }

    public async Task<IcuHospitalizedCriterionDtoResponse> UpdateAsync(Guid id, IcuHospitalizedCriterionDtoRequest req)
    {
        // Get criterion by ID
        var criterion = await uow.Repo<IcuHospitalizeCriterion>().GetByIdAsync(id, QueryFullData());
        if (criterion is null)
        {
            throw new NotFoundException(nameof(IcuHospitalizeCriterion), id);
        }

        // Map from request to model
        criterion = profile.MapModel(req, criterion);

        // Save to database
        uow.Repo<IcuHospitalizeCriterion>().Update(criterion);
        
        // Return new data
        if (await uow.SaveChangesAsync() > 0) return await GetByIdAsync(id);
        
        // Throw error if failed to save changes
        logger.LogWarning("Failed to update ICU hospitalize criterion");
        throw new InternalServerErrorException();
    }

    public async Task DeleteAsync(Guid id)
    {
        /*
         * Since TreatmentProtocol can reference directly to the criterion that the current object has,
         * we won't use cascade delete here
         */

        // Get model by ID
        var criterion = await uow.Repo<IcuHospitalizeCriterion>().GetByIdAsync(id);
        if (criterion is null)
        {
            throw new NotFoundException(nameof(IcuHospitalizeCriterion), id);
        }

        // Delete
        uow.Repo<IcuHospitalizeCriterion>().SoftDelete(criterion);
        if (await uow.SaveChangesAsync() <= 0)
        {
            logger.LogWarning("Failed to delete ICU hospitalize criterion");
            throw new InternalServerErrorException();
        }
    }
}