using BuildingBlocks.Exceptions;
using RespiraAMS.Application.Abstracts.Mappers;
using RespiraAMS.Application.Shared.Dtos;
using RespiraAMS.Domain.Enums;
using RespiraAMS.Domain.Models;

namespace RespiraAMS.Application.Shared.Mappers;

public class CreateCriterionMapper : ICreateMapper<Criterion, CreateCriterionCommand>
{
    public Criterion ToModel(CreateCriterionCommand command)
    {
        return command.Type switch
        {
            CriterionType.Boolean => new BooleanCriterion() { Name = command.Name },
            CriterionType.Numeric => new NumericCriterion()
            {
                Name = command.Name,
                Min = command.Min ?? 0,
                Max = command.Max ?? double.MaxValue,
                IsExclusive = command.IsExclusive ?? false,
                Unit = command.Unit ?? string.Empty
            },
            _ => throw new UnexpectedException("Unknown criterion type")
        };
    }
}