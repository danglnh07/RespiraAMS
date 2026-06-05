using BuildingBlocks.Exceptions;
using Microsoft.Extensions.Logging;
using RespiraAMS.Application.Abstracts.CQRS;
using RespiraAMS.Application.Abstracts.Data;
using RespiraAMS.Application.Abstracts.Mappers;
using RespiraAMS.Domain.Models;

namespace RespiraAMS.Application.Features.DiseasePathogens.CreateDiseasePathogen;

public class CreateDiseasePathogenHandler(
    IDbContext context,
    ICreateMapper<DiseasePathogen, CreateDiseasePathogenCommand> mapper,
    ILogger<CreateDiseasePathogenHandler> logger)
    : ICommandHandler<CreateDiseasePathogenCommand, CreateDiseasePathogenResult>
{
    public async Task<CreateDiseasePathogenResult> HandleAsync(CreateDiseasePathogenCommand command)
    {
        // Validate foreign keys
        if (await context.Diseases.FindAsync(command.DiseaseId) is null)
        {
            logger.LogWarning("Disease ID not found");
            throw new NotFoundException(nameof(Disease), command.DiseaseId);
        }

        if (await context.Pathogens.FindAsync(command.PathogenId) is null)
        {
            logger.LogWarning("Pathogen ID not found");
            throw new NotFoundException(nameof(Pathogen), command.PathogenId);
        }
        
        // Map from command to entity
        var diseasePathogen = mapper.ToModel(command);
        
        // Save changes to database
        await context.DiseasePathogens.AddAsync(diseasePathogen);
        if (await context.SaveChangesAsync() <= 0)
        {
            logger.LogError("Failed to create disease pathogen record");
            throw new InternalServerErrorException();
        }

        return new CreateDiseasePathogenResult(diseasePathogen.Id);
    }
}