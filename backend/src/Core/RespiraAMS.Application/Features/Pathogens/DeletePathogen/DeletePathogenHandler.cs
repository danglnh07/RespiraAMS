using BuildingBlocks.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RespiraAMS.Application.Abstracts.Data;
using RespiraAMS.Domain.Models;

namespace RespiraAMS.Application.Features.Pathogens.DeletePathogen;

public class DeletePathogenHandler(IDbContext context, ILogger<DeletePathogenHandler> logger)
{
    public async Task HandleAsync(DeletePathogenCommand command)
    {
        // Get pathogen by ID
        var pathogen = await context.Pathogens.FirstOrDefaultAsync(x => x.Id == command.Id);
        if (pathogen is null)
        {
            throw new NotFoundException(nameof(Pathogen), command.Id);
        }

        // Delete cascade in transaction
        await context.ExecuteInTransactionAsync(async () =>
        {
            // Delete pathogen
            pathogen.IsDeleted = true;
            pathogen.UpdatedAt = DateTimeOffset.UtcNow;

            // Cascade delete: ResistanceRiskFactor, DiseasePathogen, TreatmentProtocol
            var protocolCount = await context.TreatmentProtocols
                .Where(x => x.SpecialInfectionId == command.Id)
                .ExecuteUpdateAsync(x => x
                    .SetProperty(p => p.IsDeleted, true)
                    .SetProperty(p => p.UpdatedAt, DateTimeOffset.UtcNow));
            var riskCount = await context.ResistanceRiskFactors
                .Where(x => x.PathogenId == pathogen.Id)
                .ExecuteUpdateAsync(x => x
                    .SetProperty(r => r.IsDeleted, true)
                    .SetProperty(r => r.UpdatedAt, DateTimeOffset.UtcNow));
            var diseasePathogenCount = await context.DiseasePathogens
                .Where(x => x.PathogenId == pathogen.Id)
                .ExecuteUpdateAsync(x => x
                    .SetProperty(dp => dp.IsDeleted, true)
                    .SetProperty(dp => dp.UpdatedAt, DateTimeOffset.UtcNow));
            logger.LogInformation("Cascade delete pathogen: {result}", new
            {
                TreatmentProtocolCount = protocolCount,
                ResistanceRiskFactorCount = riskCount,
                DiseasePathogenCount = diseasePathogenCount
            });
        });
    }
}