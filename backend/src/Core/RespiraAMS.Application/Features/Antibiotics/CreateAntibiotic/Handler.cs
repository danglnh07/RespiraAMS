using BuildingBlocks.Exceptions;
using Microsoft.Extensions.Logging;
using RespiraAMS.Application.Abstracts.CQRS;
using RespiraAMS.Application.Abstracts.Data;
using RespiraAMS.Application.Abstracts.Mappers;
using RespiraAMS.Domain.Models;

namespace RespiraAMS.Application.Features.Antibiotics.CreateAntibiotic;

public class CreateAntibioticHandler(
    IDbContext context,
    ICreateMapper<Antibiotic, CreateAntibioticCommand> mapper,
    ILogger<CreateAntibioticHandler> logger) 
    : ICommandHandler<CreateAntibioticCommand, CreateAntibioticResult>
{
    public async Task<CreateAntibioticResult> HandleAsync(CreateAntibioticCommand command)
    {
        // Validation: check if antibiotic spectrum exists
        if (await context.AntibioticSpectra.FindAsync(command.AntibioticSpectrumId) is null)
        {
            logger.LogWarning("Antibiotic spectrum ID not found");
            throw new NotFoundException(nameof(AntibioticSpectrum), command.AntibioticSpectrumId);
        }
        
        // Map from command to model
        var antibiotic = mapper.ToModel(command);

        // Save to database
        await context.Antibiotics.AddAsync(antibiotic);
        if (await context.SaveChangesAsync() <= 0)
        {
            logger.LogError("Failed to create antibiotic");
            throw new InternalServerErrorException();
        }

        return new CreateAntibioticResult(antibiotic.Id);
    }
}