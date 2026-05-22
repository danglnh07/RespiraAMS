using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RespiraAMS.Application.Dtos;
using RespiraAMS.Application.Profiles.Implementations;
using RespiraAMS.Application.RepositoryContracts;
using RespiraAMS.Application.Services.Contracts;
using RespiraAMS.Domain.Exceptions;
using RespiraAMS.Domain.Models;

namespace RespiraAMS.Application.Services.Implementations;

public class AntibioticService(IUnitOfWork uow, AntibioticProfile profile, ILogger<AntibioticService> logger)
    : IAntibioticService
{
    private async Task IsFkExisted(Guid id)
    {
        if (await uow.Repo<AntibioticSpectrum>().GetByIdAsync(id) is null)
        {
            logger.LogWarning("Antibiotic spectrum does not exist: {SpectrumId}", id);
            throw new BadRequestException("Antibiotic spectrum does not exist");
        }
    }

    private static Func<IQueryable<Antibiotic>, IQueryable<Antibiotic>> QueryFullData()
    {
        return query => query.Include(x => x.AntibioticSpectrum);
    }

    public async Task<Guid> CreateAsync(AntibioticDtoRequest req)
    {
        // Check if the spectrum exists
        await IsFkExisted(req.AntibioticSpectrumId);

        // Map request to model
        var antibiotic = profile.ToModel(req);

        // Create entity
        await uow.Repo<Antibiotic>().CreateAsync(antibiotic);
        if (await uow.SaveChangesAsync() > 0) return antibiotic.Id;

        // Throw error if failed to save changes
        logger.LogWarning("Failed to create antibiotic");
        throw new InternalServerErrorException();
    }

    public async Task<AntibioticDtoResponse> GetByIdAsync(Guid id)
    {
        var antibiotic = await uow.Repo<Antibiotic>().GetByIdAsync(id, QueryFullData());
        return antibiotic is not null
            ? profile.ToResp(antibiotic)
            : throw new NotFoundException(nameof(Antibiotic), id);
    }

    public async Task<(PaginationMetadata, IEnumerable<AntibioticDtoResponse>)> GetListAsync(int page, int size)
    {
        var antibiotics = await uow.Repo<Antibiotic>().GetPagedListAsync(page, size, QueryFullData());
        return profile.ToPagedResponse(antibiotics);
    }

    public async Task<AntibioticDtoResponse> UpdateAsync(Guid id, AntibioticDtoRequest req)
    {
        var antibiotic = await uow.Repo<Antibiotic>().GetByIdAsync(id, QueryFullData());
        if (antibiotic is null)
        {
            throw new NotFoundException(nameof(Antibiotic), id);
        }

        // Check if spectrum ID exists
        await IsFkExisted(req.AntibioticSpectrumId);

        // Map from request to model
        antibiotic = profile.MapModel(req, antibiotic);

        // Save to database
        uow.Repo<Antibiotic>().Update(antibiotic);

        // Return with new data updated
        if (await uow.SaveChangesAsync() > 0) return await GetByIdAsync(id);

        // Throw error if failed to save changes
        logger.LogWarning("Failed to update antibiotic");
        throw new InternalServerErrorException();
    }

    public async Task DeleteAsync(Guid id)
    {
        /*
         * Even though Antibiotic and TreatmentProtocol has a many-many relationship,
         * since global query filter will shadow the join record when query TreatmentProtocol using Include,
         * we don't need to delete cascade it here
         */

        // Get antibiotic by ID
        var antibiotic = await uow.Repo<Antibiotic>().GetByIdAsync(id);
        if (antibiotic is null)
        {
            throw new NotFoundException(nameof(Antibiotic), id);
        }

        // Soft delete. 
        uow.Repo<Antibiotic>().SoftDelete(antibiotic);
        if (await uow.SaveChangesAsync() <= 0)
        {
            logger.LogWarning("Failed to delete antibiotic");
            throw new InternalServerErrorException();
        }
    }
}