using BuildingBlocks.Exceptions;
using Microsoft.Extensions.Logging;
using RespiraAMS.Application.Abstracts.CQRS;
using RespiraAMS.Application.Abstracts.Data;
using RespiraAMS.Domain.Models;

namespace RespiraAMS.Application.Features.Antibiotics.DeleteAntibiotic;

public class DeleteAntibioticHandler(IDbContext context, ILogger<DeleteAntibioticHandler> logger)
    : ICommandHandler<DeleteAntibioticCommand>
{
    public async Task HandleAsync(DeleteAntibioticCommand command)
    {
        var antibiotic = await context.Antibiotics.FindAsync(command.Id);
        if (antibiotic is null)
        {
            logger.LogWarning("Antibiotic ID not found");
            throw new NotFoundException(nameof(Antibiotic), command.Id);
        }

        antibiotic.IsDeleted = true;
        antibiotic.UpdatedAt = DateTimeOffset.UtcNow;

        if (await context.SaveChangesAsync() <= 0)
        {
            logger.LogError("Failed to delete antibiotic");
            throw new InternalServerErrorException();
        }
    }
}