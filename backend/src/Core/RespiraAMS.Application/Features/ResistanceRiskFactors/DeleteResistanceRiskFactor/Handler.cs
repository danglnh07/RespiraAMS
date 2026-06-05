using BuildingBlocks.Exceptions;
using Microsoft.Extensions.Logging;
using RespiraAMS.Application.Abstracts.CQRS;
using RespiraAMS.Application.Abstracts.Data;
using RespiraAMS.Domain.Models;

namespace RespiraAMS.Application.Features.ResistanceRiskFactors.DeleteResistanceRiskFactor;

public class DeleteResistanceRiskFactorHandler(IDbContext context, ILogger<DeleteResistanceRiskFactorHandler> logger)
    : ICommandHandler<DeleteResistanceRiskFactorCommand>
{
    public async Task HandleAsync(DeleteResistanceRiskFactorCommand command)
    {
        // Get entity by ID
        var risk = await context.ResistanceRiskFactors.FindAsync(command.Id);
        if (risk is null)
        {
            logger.LogWarning("Resistance risk factor not found");
            throw new NotFoundException(nameof(ResistanceRiskFactor), command.Id);
        }

        // Delete entity
        risk.IsDeleted = true;
        risk.UpdatedAt = DateTimeOffset.UtcNow;
        if (await context.SaveChangesAsync() <= 0)
        {
            logger.LogError("Failed to delete resistance risk factor");
            throw new InternalServerErrorException();
        }
    }
}