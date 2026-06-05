using RespiraAMS.Application.Abstracts.Mappers;
using RespiraAMS.Application.Shared.Dtos;
using RespiraAMS.Domain.Models;

namespace RespiraAMS.Application.Features.IcuHospitalizeCriteria.CreateIcuHospitalizeCriterion;

public class CreateIcuHospitalizeCriterionMapper(ICreateMapper<Criterion, CreateCriterionCommand> criterionMapper)
    : ICreateMapper<IcuHospitalizeCriterion, CreateIcuHospitalizeCriterionCommand>
{
    public IcuHospitalizeCriterion ToModel(CreateIcuHospitalizeCriterionCommand command)
    {
        return new IcuHospitalizeCriterion
        {
            DiseaseId = command.DiseaseId,
            IsMainCriteria = command.IsMainCriteria,
            Criterion = criterionMapper.ToModel(command.Criterion),
        };
    }
}