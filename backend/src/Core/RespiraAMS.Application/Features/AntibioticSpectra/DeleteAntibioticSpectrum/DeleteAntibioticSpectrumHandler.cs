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
        var spectrum = await context.AntibioticSpectra.FirstOrDefaultAsync(x => x.Id == command.Id);
        if (spectrum is null)
        {
            throw new BadRequestException("Antibiotic spectrum not found");
        }
        
        // Start delete with cascade transaction
        await context.ExecuteInTransactionAsync(() =>
        {
            // Delete spectrum
            spectrum.IsDeleted = true;
            spectrum.UpdatedAt = DateTimeOffset.UtcNow;
            
            // Cascade delete antibiotic
            logger.LogInformation("Start cascade delete antibiotic: {count}", spectrum.Antibiotics.Count);
            foreach (var antibiotic in spectrum.Antibiotics)
            {
                antibiotic.IsDeleted = false;
                antibiotic.UpdatedAt = DateTimeOffset.Now;
            }
        });
    }
}