using RespiraAMS.Application.Abstracts.Mappers;
using RespiraAMS.Application.Shared.Dtos;
using RespiraAMS.Application.Shared.Mappers;
using RespiraAMS.Domain.Models;

namespace RespiraAMS.Application.Features.IcuHospitalizeCriteria.UpdateIcuHospitalizeCriterion;

public class UpdateIcuHospitalizeCriterionMapper(IUpdateMapper<Criterion, UpdateCriterionCommand> criterionMapper)
    : IUpdateMapper<IcuHospitalizeCriterion, UpdateIcuHospitalizeCriterionCommand>
{
    public void MapModel(IcuHospitalizeCriterion model, UpdateIcuHospitalizeCriterionCommand command)
    {
        model.IsMainCriteria = command.IsMainCriteria;
        criterionMapper.MapModel(model.Criterion, command.Criterion);
        model.UpdatedAt = DateTimeOffset.UtcNow;
    }
}