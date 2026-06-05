using BuildingBlocks.Exceptions;
using Microsoft.Extensions.Logging;
using RespiraAMS.Application.Abstracts.CQRS;
using RespiraAMS.Application.Abstracts.Data;
using RespiraAMS.Application.Abstracts.Mappers;
using RespiraAMS.Domain.Models;

namespace RespiraAMS.Application.Features.Diseases.CreateDisease;

public class CreateDiseaseHandler(
    IDbContext context,
    ICreateMapper<Disease, CreateDiseaseCommand> mapper,
    ILogger<CreateDiseaseHandler> logger)
    : ICommandHandler<CreateDiseaseCommand, CreateDiseaseResult>
{
    public async Task<CreateDiseaseResult> HandleAsync(CreateDiseaseCommand command)
    {
        // Map from command to model
        var disease = mapper.ToModel(command);
        
        // Save changes to database
        await context.Diseases.AddAsync(disease);
        if (await context.SaveChangesAsync() <= 0)
        {
            logger.LogError("Failed to create disease");
            throw new InternalServerErrorException();
        }

        return new CreateDiseaseResult(disease.Id);
    }
}