using RespiraAMS.Application.Abstracts.Mappers;
using RespiraAMS.Application.Shared.Dtos;
using RespiraAMS.Domain.Enums;
using RespiraAMS.Domain.Models;

namespace RespiraAMS.Application.Shared.Mappers;

public class CriterionResultMapper : IResultMapper<Criterion, CriterionItem>
{
    public CriterionItem ToResult(Criterion model)
    {
        return new CriterionItem()
        {
            Id = model.Id,
            Name = model.Name,
            Type = model.Type,
            Min = model.Type == CriterionType.Numeric ? ((NumericCriterion)model).Min : null,
            Max = model.Type == CriterionType.Numeric ? ((NumericCriterion)model).Max : null,
            Unit = model.Type == CriterionType.Numeric ? ((NumericCriterion)model).Unit : null,
            IsExclusive = model.Type == CriterionType.Numeric
                ? ((NumericCriterion)model).IsExclusive
                : null
        };
    }
}