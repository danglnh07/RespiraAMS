using BuildingBlocks.Exceptions;
using Microsoft.Extensions.Logging;
using RespiraAMS.Application.Abstracts.CQRS;
using RespiraAMS.Application.Abstracts.Data;
using RespiraAMS.Application.Abstracts.Mappers;
using RespiraAMS.Domain.Models;

namespace RespiraAMS.Application.Features.ResistanceRiskFactors.CreateResistanceRiskFactor;

public class CreateResistanceRiskFactorHandler(
    IDbContext context,
    ICreateMapper<ResistanceRiskFactor, CreateResistanceRiskFactorCommand> mapper,
    ILogger<CreateResistanceRiskFactorHandler> logger)
    : ICommandHandler<CreateResistanceRiskFactorCommand, CreateResistanceRiskFactorResult>
{
    public async Task<CreateResistanceRiskFactorResult> HandleAsync(CreateResistanceRiskFactorCommand command)
    {
        // Validate FKs
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
        
        // Map from command to query
        var risk = mapper.ToModel(command);
        
        // Save changes to database
        await context.ResistanceRiskFactors.AddAsync(risk);
        if (await context.SaveChangesAsync() <= 0)
        {
            logger.LogError("Failed to create resistance risk factor");
            throw new InternalServerErrorException();
        }

        return new CreateResistanceRiskFactorResult(risk.Id);
    }
}