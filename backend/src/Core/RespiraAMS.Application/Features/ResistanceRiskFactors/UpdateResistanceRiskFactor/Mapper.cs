using RespiraAMS.Application.Abstracts.Mappers;
using RespiraAMS.Application.Shared.Dtos;
using RespiraAMS.Domain.Models;

namespace RespiraAMS.Application.Features.ResistanceRiskFactors.UpdateResistanceRiskFactor;

public class UpdateResistanceRiskFactorMapper(IUpdateMapper<Criterion, UpdateCriterionCommand> criterionMapper)
    : IUpdateMapper<ResistanceRiskFactor, UpdateResistanceRiskFactorCommand>
{
    public void MapModel(ResistanceRiskFactor model, UpdateResistanceRiskFactorCommand command)
    {
        model.Name = command.Name;
        model.PathogenId = command.PathogenId;
        criterionMapper.MapModel(model.Criterion, command.Criterion);
        model.UpdatedAt = DateTimeOffset.UtcNow;
    }
}