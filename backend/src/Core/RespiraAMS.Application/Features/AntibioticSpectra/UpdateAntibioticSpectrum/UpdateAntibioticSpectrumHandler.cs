using BuildingBlocks.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RespiraAMS.Application.Abstracts.Data;
using RespiraAMS.Application.Mappers;

namespace RespiraAMS.Application.Features.AntibioticSpectra.UpdateAntibioticSpectrum;

public class UpdateAntibioticSpectrumHandler(
    IDbContext context,
    AntibioticSpectrumMapper mapper,
    ILogger<UpdateAntibioticSpectrumHandler> logger)
{
    public async Task<UpdateAntibioticSpectrumResult> HandleAsync(UpdateAntibioticSpectrumCommand command)
    {
        // Get entity from database
        var spectrum = await context.AntibioticSpectra.FirstOrDefaultAsync(x => x.Id == command.Id);
        if (spectrum is null)
        {
            throw new BadRequestException("Antibiotic spectrum not found");
        }

        // Map command to entity
        spectrum = mapper.ToModel(spectrum, command);

        // Save changes
        if (await context.SaveChangesAsync() > 0) return mapper.ToResult(spectrum); 

        // Log and throw error if failed to save changes
        logger.LogWarning("Failed to update antibiotic spectrum");
        throw new InternalServerErrorException();
    }
}