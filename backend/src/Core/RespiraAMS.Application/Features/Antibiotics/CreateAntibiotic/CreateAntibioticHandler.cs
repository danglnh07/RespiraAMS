using BuildingBlocks.Exceptions;
using Microsoft.Extensions.Logging;
using RespiraAMS.Application.Abstracts.Data;
using RespiraAMS.Application.Features.Antibiotics.Shared;
using RespiraAMS.Application.Mappers;

namespace RespiraAMS.Application.Features.Antibiotics.CreateAntibiotic;

public class CreateAntibioticHandler(
    IDbContext context,
    AntibioticMapper mapper,
    ILogger<CreateAntibioticHandler> logger)
{
    public async Task<CreateAntibioticResult> HandleAsync(CreateAntibioticCommand command)
    {
        // Validation: check if antibiotic spectrum exists
        if (!await SharedValidation.IsAntibioticSpectrumIdExists(context, command.AntibioticSpectrumId))
        {
            logger.LogWarning("Antibiotic spectrum does not exist: {SpectrumId}", command.AntibioticSpectrumId);
            throw new BadRequestException("Antibiotic spectrum does not exist");
        }
        
        // Map from command to model
        var antibiotic = mapper.ToModel(command);

        // Save to database
        await context.Antibiotics.AddAsync(antibiotic);
        if (await context.SaveChangesAsync() <= 0)
        {
            logger.LogWarning("Failed to create antibiotic");
            throw new InternalServerErrorException();
        }

        return new CreateAntibioticResult(antibiotic.Id);
    }
}