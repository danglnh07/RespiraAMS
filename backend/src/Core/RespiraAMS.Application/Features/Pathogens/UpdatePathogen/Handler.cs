using BuildingBlocks.Exceptions;
using Microsoft.Extensions.Logging;
using RespiraAMS.Application.Abstracts.CQRS;
using RespiraAMS.Application.Abstracts.Data;
using RespiraAMS.Application.Abstracts.Mappers;
using RespiraAMS.Domain.Models;

namespace RespiraAMS.Application.Features.Pathogens.UpdatePathogen;

public class UpdatePathogenHandler(
    IDbContext context,
    IUpdateMapper<Pathogen, UpdatePathogenCommand> mapper,
    ILogger<UpdatePathogenHandler> logger)
    : ICommandHandler<UpdatePathogenCommand>
{
    public async Task HandleAsync(UpdatePathogenCommand command)
    {
        // Get pathogen by ID
        var pathogen = await context.Pathogens.FindAsync(command.Id);
        if (pathogen is null)
        {
            logger.LogWarning("Pathogen ID not found");
            throw new NotFoundException(nameof(Pathogen), command.Id);
        }
        
        // Map command to model
        mapper.MapModel(pathogen, command);
        
        // Save changes to database
        await context.Pathogens.AddAsync(pathogen);
        if (await context.SaveChangesAsync() <= 0)
        {
            logger.LogError("Failed to update pathogen");
            throw new InternalServerErrorException();
        }
    }
}