using BuildingBlocks.Exceptions;
using Microsoft.Extensions.Logging;
using RespiraAMS.Application.Abstracts.CQRS;
using RespiraAMS.Application.Abstracts.Data;
using RespiraAMS.Application.Abstracts.Mappers;
using RespiraAMS.Domain.Models;

namespace RespiraAMS.Application.Features.AntibioticSpectra.UpdateAntibioticSpectrum;

public class UpdateAntibioticSpectrumHandler(
    IDbContext context,
    IUpdateMapper<AntibioticSpectrum, UpdateAntibioticSpectrumCommand> mapper,
    ILogger<UpdateAntibioticSpectrumHandler> logger) 
    : ICommandHandler<UpdateAntibioticSpectrumCommand>
{
    public async Task HandleAsync(UpdateAntibioticSpectrumCommand command)
    {
        // Get entity from database
        var spectrum = await context.AntibioticSpectra.FindAsync(command.Id);
        if (spectrum is null)
        {
            logger.LogWarning("Antibiotic spectrum ID not found");
            throw new NotFoundException(nameof(AntibioticSpectrum), command.Id);
        }

        // Map command to entity
        mapper.MapModel(spectrum, command);

        // Save changes
        if (await context.SaveChangesAsync() <= 0)
        {
            logger.LogError("Failed to update antibiotic spectrum");
            throw new InternalServerErrorException();
        }
    }
}