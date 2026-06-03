using BuildingBlocks.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RespiraAMS.Application.Abstracts.Data;
using RespiraAMS.Application.Features.Antibiotics.Shared;
using RespiraAMS.Application.Mappers;
using RespiraAMS.Domain.Models;

namespace RespiraAMS.Application.Features.Antibiotics.UpdateAntibiotic;

public class UpdateAntibioticHandler(
    IDbContext context,
    AntibioticMapper mapper,
    ILogger<UpdateAntibioticHandler> logger)
{
    public async Task<UpdateAntibioticResult> HandleAsync(UpdateAntibioticCommand command)
    {   
        // Validation: check if antibiotic spectrum exists
        if (!await SharedValidation.IsAntibioticSpectrumIdExists(context, command.AntibioticSpectrumId))
        {
            logger.LogWarning("Antibiotic spectrum does not exist: {SpectrumId}", command.AntibioticSpectrumId);
            throw new BadRequestException("Antibiotic spectrum does not exist");
        }
        
        // Get antibiotic by ID
        var antibiotic = await context.Antibiotics.FindAsync(command.Id);
        if (antibiotic is null)
        {
            throw new NotFoundException(nameof(Antibiotic), command.Id);
        }
        
        // Map command to model
        antibiotic = mapper.ToModel(antibiotic, command);
        
        // Save changes to database
        if (await context.SaveChangesAsync() <= 0)
        {
            logger.LogWarning("Failed to update antibiotic");
            throw new InternalServerErrorException();
        }

        return mapper.ToResult(antibiotic);
    }
}