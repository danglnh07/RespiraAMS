using BuildingBlocks.Exceptions;
using Microsoft.Extensions.Logging;
using RespiraAMS.Application.Abstracts.CQRS;
using RespiraAMS.Application.Abstracts.Data;
using RespiraAMS.Application.Abstracts.Mappers;
using RespiraAMS.Domain.Models;

namespace RespiraAMS.Application.Features.AntibioticSpectra.CreateAntibioticSpectrum;

public class CreateAntibioticSpectrumHandler(
    IDbContext context,
    ICreateMapper<AntibioticSpectrum, CreateAntibioticSpectrumCommand> mapper,
    ILogger<CreateAntibioticSpectrumHandler> logger)
    : ICommandHandler<CreateAntibioticSpectrumCommand, CreateAntibioticSpectrumResult>
{
    public async Task<CreateAntibioticSpectrumResult> HandleAsync(CreateAntibioticSpectrumCommand command)
    {
        // Create antibiotic spectrum
        var spectrum = mapper.ToModel(command);

        // Save to database
        await context.AntibioticSpectra.AddAsync(spectrum);
        if (await context.SaveChangesAsync() <= 0)
        {
            logger.LogError("Failed to create antibiotic spectrum");
            throw new InternalServerErrorException();
        }

        return new CreateAntibioticSpectrumResult(spectrum.Id);
    }
}