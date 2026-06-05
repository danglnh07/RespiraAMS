using RespiraAMS.Application.Abstracts.Mappers;
using RespiraAMS.Application.Shared.Dtos;
using RespiraAMS.Domain.Models;

namespace RespiraAMS.Application.Features.ResistanceRiskFactors.CreateResistanceRiskFactor;

public class CreateResistanceRiskFactorMapper(ICreateMapper<Criterion, CreateCriterionCommand> criterionMapper)
    : ICreateMapper<ResistanceRiskFactor, CreateResistanceRiskFactorCommand>
{
    public ResistanceRiskFactor ToModel(CreateResistanceRiskFactorCommand command)
    {
        return new ResistanceRiskFactor()
        {
            Name = command.Name,
            DiseaseId = command.DiseaseId,
            PathogenId = command.PathogenId,
            Criterion = criterionMapper.ToModel(command.Criterion),
        };
    }
}