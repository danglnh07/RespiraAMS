using BuildingBlocks.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RespiraAMS.Application.Abstracts.CQRS;
using RespiraAMS.Application.Abstracts.Data;
using RespiraAMS.Application.Abstracts.Mappers;
using RespiraAMS.Domain.Models;

namespace RespiraAMS.Application.Features.TreatmentProtocols.UpdateTreatmentProtocol;

public class UpdateTreatmentProtocolHandler(
    IDbContext context,
    IUpdateMapper<TreatmentProtocol, UpdateTreatmentProtocolCommand> mapper,
    ILogger<UpdateTreatmentProtocolHandler> logger) 
    : ICommandHandler<UpdateTreatmentProtocolCommand>
{
    public async Task HandleAsync(UpdateTreatmentProtocolCommand command)
    {
        // Validate FKs
        if (command.SpecialInfectionId is not null &&
            await context.Pathogens.FindAsync(command.SpecialInfectionId) is null)
        {
            logger.LogWarning("Special Infection ID (Pathogen ID) not found");
            throw new NotFoundException(nameof(Pathogen), command.SpecialInfectionId.Value);
        }

        if (await context.Criteria.CountAsync(x => command.OtherCriteriaIds.Contains(x.Id)) !=
            command.OtherCriteriaIds.Count)
        {
            logger.LogWarning("Not all criterion ids exists");
            throw new BadRequestException("Not all criterion ids exists");
        }

        if (await context.Antibiotics.CountAsync(x => command.MedicineIds.Contains(x.Id)) != command.MedicineIds.Count)
        {
            logger.LogWarning("Not all antibiotic ids exists");
            throw new BadRequestException("Not all medicine ids exists");
        }
        
        // Get treatment protocol by ID
        var protocol = await context.TreatmentProtocols.FindAsync(command.Id);
        if (protocol is null)
        {
            logger.LogWarning("Treatment protocol ID not found");
            throw new NotFoundException(nameof(TreatmentProtocol), command.Id);
        }
        
        // Map from command to model
        mapper.MapModel(protocol, command);
        
        // Add stub to the IDs list
        context.UpdateRelations(protocol.Medicines, command.MedicineIds);
        context.UpdateRelations(protocol.OtherCriteria, command.OtherCriteriaIds);
        
        // Save changes to database
        if (await context.SaveChangesAsync() <= 0)
        {
            logger.LogError("Failed to update treatment protocol");
            throw new InternalServerErrorException();
        }
    }
}