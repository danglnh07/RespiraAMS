using BuildingBlocks.Exceptions;
using RespiraAMS.Application.Abstracts.Mappers;
using RespiraAMS.Application.Shared.Dtos;
using RespiraAMS.Domain.Enums;
using RespiraAMS.Domain.Models;

namespace RespiraAMS.Application.Shared.Mappers;

public class UpdateCriterionMapper : IUpdateMapper<Criterion, UpdateCriterionCommand>
{
    public void MapModel(Criterion model, UpdateCriterionCommand command)
    {
        if (model.Type != command.Type)
        {
            throw new BadRequestException("Criterion type mismatch: criterion type does not allow for changes");
        }

        model.Name = command.Name;
        switch (model.Type)
        {
            case CriterionType.Boolean:
                break;
            case CriterionType.Numeric:
                ((NumericCriterion)model).Max = command.Max ?? 0;
                ((NumericCriterion)model).Min = command.Min ?? 0;
                ((NumericCriterion)model).Unit = command.Unit ?? "";
                ((NumericCriterion)model).IsExclusive = command.IsExclusive ?? false;
                break;
            default:
                throw new UnexpectedException("Unexpected type for criterion");
        }

        model.UpdatedAt = DateTimeOffset.UtcNow;
        // Criterion type must not change 
    }
}