using RespiraAMS.Application.Abstracts.Mappers;
using RespiraAMS.Domain.Models;

namespace RespiraAMS.Application.Features.Diseases.CreateDisease;

public class CreateDiseaseMapper : ICreateMapper<Disease, CreateDiseaseCommand>
{
    public Disease ToModel(CreateDiseaseCommand command)
    {
        return new Disease
        {
            Name = command.Name,
            Description = command.Description,
            RequiredIcuMainCriteria = command.RequiredIcuMainCriteria,
            RequiredIcuSecondaryCriteria = command.RequiredIcuSecondaryCriteria,
        };
    }
}