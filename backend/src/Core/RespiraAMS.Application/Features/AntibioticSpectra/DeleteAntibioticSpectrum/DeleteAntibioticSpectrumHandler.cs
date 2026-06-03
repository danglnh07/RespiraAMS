using BuildingBlocks.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RespiraAMS.Application.Abstracts.Data;

namespace RespiraAMS.Application.Features.AntibioticSpectra.DeleteAntibioticSpectrum;

public class DeleteAntibioticSpectrumHandler(IDbContext context, ILogger<DeleteAntibioticSpectrumHandler> logger)
{
    public async Task HandleAsync(DeleteAntibioticSpectrumCommand command)
    {
        // Get entity from database
        var spectrum = await context.AntibioticSpectra.FindAsync(command.Id);
        if (spectrum is null)
        {
            throw new BadRequestException("Antibiotic spectrum not found");
        }

        // Start delete with cascade transaction
        await context.ExecuteInTransactionAsync(async () =>
        {
            // Delete spectrum
            spectrum.IsDeleted = true;
            spectrum.UpdatedAt = DateTimeOffset.UtcNow;

            // Cascade delete antibiotic
            var count = await context.Antibiotics
                .Where(x => x.AntibioticSpectrumId == command.Id)
                .ExecuteUpdateAsync(x => x
                    .SetProperty(a => a.IsDeleted, true)
                    .SetProperty(a => a.UpdatedAt, DateTimeOffset.UtcNow));
            logger.LogInformation("Cascade delete antibiotic spectrum: deleted {count} antibiotic", count);
        });
    }
}