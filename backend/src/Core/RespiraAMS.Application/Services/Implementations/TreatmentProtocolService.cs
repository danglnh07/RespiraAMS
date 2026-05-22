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

public class TreatmentProtocolService(
    IUnitOfWork uow,
    TreatmentProtocolProfile profile,
    CriterionProfile criterionProfile,
    ILogger<TreatmentProtocolService> logger) : ITreatmentProtocolService
{
    private async Task AreFkExisted(Guid diseaseId, Guid? pathogenId, List<Guid>? criterionIds, List<Guid> medicineIds)
    {
        if (await uow.Repo<Disease>().GetByIdAsync(diseaseId) is null)
        {
            logger.LogWarning("Disease ID not exist: {DiseaseID}", diseaseId);
            throw new BadRequestException("Disease ID not exists");
        }

        if (pathogenId is not null && await uow.Repo<Pathogen>().GetByIdAsync(pathogenId.Value) is null)
        {
            logger.LogWarning("Special Infection (Pathogen) ID not exist: {PathogenID}", pathogenId);
            throw new BadRequestException("Special Infection (Pathogen) ID not exists");
        }

        if (criterionIds is not null && !await uow.Repo<Criterion>().AllIdsExistAsync(criterionIds))
        {
            logger.LogWarning("Not all criterion ids exist: {CriterionIds}", criterionIds);
            throw new BadRequestException("Not all criterion ids exists");
        }

        if (!await uow.Repo<Antibiotic>().AllIdsExistAsync(medicineIds))
        {
            logger.LogWarning("Not all medicine ids exist: {MedicineIds}", medicineIds);
            throw new BadRequestException("Not all medicine ids exists");
        }
    }

    private static Func<IQueryable<TreatmentProtocol>, IQueryable<TreatmentProtocol>> QueryFullData()
    {
        return query => query
            .AsSplitQuery() // Performance for multiple include
            .Include(x => x.Medicines)
            .ThenInclude(x => x.AntibioticSpectrum)
            .Include(x => x.SpecialInfection)
            .Include(x => x.OtherCriteria);
    }

    public async Task<Guid> CreateAsync(TreatmentProtocolDtoRequest req)
    {
        // Check if all Ids (foreign keys) exists
        await AreFkExisted(req.DiseaseId, req.SpecialInfectionId, req.OtherCriteriaIds, req.MedicineIds);

        // Map request to model        
        // Since we have many-many relationships, creating them require fetching associated data -> N + 1 query,
        // so we use a stub strategy to reduce database call
        var protocol = profile.ToModel(req);
        uow.Repo<Antibiotic>().UpdateRelations(protocol.Medicines, req.MedicineIds);
        uow.Repo<Criterion>().UpdateRelations(protocol.OtherCriteria, req.OtherCriteriaIds);
        // protocol.Medicines.UpdateRelations(req.MedicineIds, uow.Repo<Antibiotic>());
        // protocol.OtherCriteria.UpdateRelations(req.OtherCriteriaIds, uow.Repo<Criterion>());

        // Save changes to database
        await uow.Repo<TreatmentProtocol>().CreateAsync(protocol);

        // Return ID
        if (await uow.SaveChangesAsync() > 0) return protocol.Id;

        // Throw error if failed to save changes to database
        logger.LogWarning("Failed to create treatment protocol");
        throw new InternalServerErrorException();
    }

    public async Task<TreatmentProtocolDtoResponse> GetByIdAsync(Guid id)
    {
        var protocol = await uow.Repo<TreatmentProtocol>().GetByIdAsync(id, QueryFullData());
        return protocol is null ? throw new NotFoundException(nameof(TreatmentProtocol), id) : profile.ToResp(protocol);
    }

    public async Task<(PaginationMetadata, IEnumerable<TreatmentProtocolDtoResponse>)> GetListAsync(int page, int size)
    {
        var protocols = await uow.Repo<TreatmentProtocol>()
            .GetPagedListAsync(page, size, QueryFullData());
        return profile.ToPagedResponse(protocols);
    }

    public async Task<TreatmentProtocolDtoResponse> UpdateAsync(Guid id, TreatmentProtocolDtoRequest req)
    {
        // Get protocol by ID
        var protocol = await uow.Repo<TreatmentProtocol>().GetByIdAsync(id, QueryFullData());
        if (protocol is null)
        {
            throw new NotFoundException(nameof(TreatmentProtocol), id);
        }

        // Check if all Ids (foreign keys) exists
        await AreFkExisted(req.DiseaseId, req.SpecialInfectionId, req.OtherCriteriaIds, req.MedicineIds);

        // Map request to model
        // Since we have many-many relationships, updating them require fetching associated data -> N + 1 query,
        // so we use a stub strategy to reduce database call
        protocol = profile.MapModel(req, protocol);
        uow.Repo<Antibiotic>().UpdateRelations(protocol.Medicines, req.MedicineIds);
        uow.Repo<Criterion>().UpdateRelations(protocol.OtherCriteria, req.OtherCriteriaIds);
        // protocol.OtherCriteria.UpdateRelations(req.OtherCriteriaIds, uow.Repo<Criterion>());
        // protocol.Medicines.UpdateRelations(req.MedicineIds, uow.Repo<Antibiotic>());

        // Save changes to database
        uow.Repo<TreatmentProtocol>().Update(protocol);
        if (await uow.SaveChangesAsync() <= 0)
        {
            logger.LogWarning("Failed to update treatment protocol");
            throw new InternalServerErrorException();
        }

        // Because new medicine include spectrum, and we only add the medicine into stub,
        // which make the change tracker didn't track the spectrum of the new medicines
        // added -> spectrum null -> map failed.
        // So, we have 2 options:
        // 1. Add spectrum to stub manually,
        // 2. Fetch new data directly from database (bypassing change tracker)
        // It easier to implement the second method, so we'll choose it here
        // NOTE: the CreateAsync method actually had the same issue, but since only return the ID, not
        // a whole response object, it doesn't matter
        var updated = await ((ITreatmentProtocolRepository)uow.Repo<TreatmentProtocol>())
            .GetTreatmentProtocolByIdAsNoTrackingAsync(id, QueryFullData());

        // We'll add null check for safety guard, even if it's unlikely to be null
        return updated is null ? throw new InternalServerErrorException() : profile.ToResp(updated);
    }

    public async Task DeleteAsync(Guid id)
    {
        // Get model by ID
        var protocol = await uow.Repo<TreatmentProtocol>().GetByIdAsync(id);
        if (protocol is null)
        {
            throw new NotFoundException(nameof(TreatmentProtocol), id);
        }

        // Delete 
        uow.Repo<TreatmentProtocol>().SoftDelete(protocol);
        if (await uow.SaveChangesAsync() <= 0)
        {
            logger.LogWarning("Failed to delete treatment protocol");
            throw new InternalServerErrorException();
        }
    }

    public async Task AddCriteriaToProtocol(Guid id, List<CriterionDtoRequest> req)
    {
        // Get treatment protocol by ID
        var protocol = await uow.Repo<TreatmentProtocol>()
            .GetByIdAsync(id, query => query.Include(x => x.OtherCriteria));
        if (protocol is null)
        {
            throw new NotFoundException(nameof(TreatmentProtocol), id);
        }

        // Map request to models
        var criteria = req.Select(criterionProfile.ToModel).ToList();

        // Start transaction
        await uow.ExecuteInTransactionAsync(async () =>
        {
            // Add batch
            var success = await ((ICriterionRepository)uow.Repo<Criterion>()).CreateCriteriaAsync(criteria);
            logger.LogInformation("Added {successCount} criteria into treatment protocol", success);

            // Add the created list of criteria into the treatment protocol list
            // Since criteria list is already tracked by EF Core, we don't need to use stub
            protocol.OtherCriteria.AddRange(criteria);

            // Save changes to database
            uow.Repo<TreatmentProtocol>().Update(protocol);
        });
    }
}