using BuildingBlocks.Exceptions;
using Microsoft.Extensions.Logging;
using RespiraAMS.Application.Abstracts.CQRS;
using RespiraAMS.Application.Abstracts.Data;
using RespiraAMS.Application.Abstracts.Mappers;
using RespiraAMS.Domain.Models;

namespace RespiraAMS.Application.Features.Pathogens.CreatePathogen;

public class CreatePathogenHandler(
    IDbContext context,
    ICreateMapper<Pathogen, CreatePathogenCommand> mapper,
    ILogger<CreatePathogenHandler> logger)
    : ICommandHandler<CreatePathogenCommand, CreatePathogenResult>
{
    public async Task<CreatePathogenResult> HandleAsync(CreatePathogenCommand command)
    {
        // Map command to model
        var pathogen = mapper.ToModel(command);

        // Save changes to database
        await context.Pathogens.AddAsync(pathogen);
        if (await context.SaveChangesAsync() <= 0)
        {
            logger.LogError("Failed to create pathogen");
            throw new InternalServerErrorException();
        }

        return new CreatePathogenResult(pathogen.Id);
    }
}