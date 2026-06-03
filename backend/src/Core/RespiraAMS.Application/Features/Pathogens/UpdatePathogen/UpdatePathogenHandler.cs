using BuildingBlocks.Exceptions;
using Microsoft.Extensions.Logging;
using RespiraAMS.Application.Abstracts.Data;
using RespiraAMS.Application.Mappers;
using RespiraAMS.Domain.Models;

namespace RespiraAMS.Application.Features.Pathogens.UpdatePathogen;

public class UpdatePathogenHandler(IDbContext context, PathogenMapper mapper, ILogger<UpdatePathogenHandler> logger)
{
    public async Task<UpdatePathogenResult> HandleAsync(UpdatePathogenCommand command)
    {
        // Get pathogen by ID
        var pathogen = await context.Pathogens.FindAsync(command.Id);
        if (pathogen is null)
        {
            throw new NotFoundException(nameof(Pathogen), command.Id);
        }
        
        // Map command to model
        pathogen = mapper.ToModel(pathogen, command);
        
        // Save changes to database
        await context.Pathogens.AddAsync(pathogen);
        if (await context.SaveChangesAsync() <= 0)
        {
            logger.LogWarning("Failed to update pathogen");
            throw new InternalServerErrorException();
        }

        return mapper.ToResult(pathogen);
    }
}