using BuildingBlocks.Exceptions;
using Microsoft.Extensions.Logging;
using RespiraAMS.Application.Abstracts.Data;
using RespiraAMS.Application.Mappers;

namespace RespiraAMS.Application.Features.AntibioticSpectra.CreateAntibioticSpectrum;

public class CreateAntibioticSpectrumHandler(
    IDbContext context,
    AntibioticSpectrumMapper mapper,
    ILogger<CreateAntibioticSpectrumHandler> logger)
{
    public async Task<CreateAntibioticSpectrumResult> HandleAsync(CreateAntibioticSpectrumCommand command)
    {
        // Create antibiotic spectrum
        var spectrum = mapper.ToModel(command);

        // Save to database
        await context.AntibioticSpectra.AddAsync(spectrum);
        if (await context.SaveChangesAsync() <= 0)
        {
            logger.LogWarning("Failed to create antibiotic spectrum");
            throw new InternalServerErrorException();
        }

        return new CreateAntibioticSpectrumResult(spectrum.Id);
    }
}