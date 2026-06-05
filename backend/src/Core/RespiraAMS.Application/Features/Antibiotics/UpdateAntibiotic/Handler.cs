using BuildingBlocks.Exceptions;
using Microsoft.Extensions.Logging;
using RespiraAMS.Application.Abstracts.CQRS;
using RespiraAMS.Application.Abstracts.Data;
using RespiraAMS.Application.Abstracts.Mappers;
using RespiraAMS.Domain.Models;

namespace RespiraAMS.Application.Features.Antibiotics.UpdateAntibiotic;

public class UpdateAntibioticHandler(
    IDbContext context,
    IUpdateMapper<Antibiotic, UpdateAntibioticCommand> mapper,
    ILogger<UpdateAntibioticHandler> logger) 
    : ICommandHandler<UpdateAntibioticCommand>
{
    public async Task HandleAsync(UpdateAntibioticCommand command)
    {
        // Validation: check if antibiotic spectrum exists
        if (await context.AntibioticSpectra.FindAsync(command.AntibioticSpectrumId) is null)
        {
            logger.LogWarning("Antibiotic spectrum not exists");
            throw new NotFoundException(nameof(AntibioticSpectrum), command.AntibioticSpectrumId);
        }
        
        // Get antibiotic by ID
        var antibiotic = await context.Antibiotics.FindAsync(command.Id);
        if (antibiotic is null)
        {
            throw new NotFoundException(nameof(Antibiotic), command.Id);
        }
        
        // Map command to model
        mapper.MapModel(antibiotic, command);
        
        // Save changes to database
        if (await context.SaveChangesAsync() <= 0)
        {
            logger.LogError("Failed to update antibiotic");
            throw new InternalServerErrorException();
        }
    }
}