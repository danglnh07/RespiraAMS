using BuildingBlocks.Exceptions;
using Microsoft.Extensions.Logging;
using RespiraAMS.Application.Abstracts.Data;
using RespiraAMS.Application.Mappers;

namespace RespiraAMS.Application.Features.Pathogens.CreatePathogen;

public class CreatePathogenHandler(IDbContext context, PathogenMapper mapper, ILogger<CreatePathogenHandler> logger)
{
    public async Task<CreatePathogenResult> HandleAsync(CreatePathogenCommand command)
    {
        // Map command to model
        var pathogen = mapper.ToModel(command);
        
        // Save changes to database
        await context.Pathogens.AddAsync(pathogen);
        if (await context.SaveChangesAsync() <= 0)
        {
            logger.LogWarning("Failed to create pathogen");
            throw new InternalServerErrorException();
        }

        return new CreatePathogenResult(pathogen.Id);
    }
}