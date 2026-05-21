using RespiraAMS.Core.Dtos;
using RespiraAMS.Core.Enums;
using RespiraAMS.Core.Models;
using RespiraAMS.Core.Profiles;
using X.PagedList;
using X.PagedList.Extensions;

namespace RespiraAMS.Infrastructure.Profiles;

public class CriterionProfile : IProfile<CriterionDtoRequest, Criterion, CriterionDtoResponse>
{
    public Criterion ToModel(CriterionDtoRequest req)
    {
        return req.Type switch
        {
            CriterionType.Boolean => new BooleanCriterion() { Name = req.Name, },
            CriterionType.Numeric => new NumericCriterion()
            {
                Name = req.Name,
                Min = req.Min ?? 0,
                Max = req.Max ?? 0,
                Unit =  req.Unit ?? "",
                IsExclusive = req.IsExclusive ?? false,
            },
            _ => throw new Exception("Unknown Criterion type")
        };
    }

    public Criterion MapModel(CriterionDtoRequest req, Criterion model)
    {
        switch (req.Type)
        {
            case CriterionType.Boolean:
                model.Name = req.Name;
                break;
            case CriterionType.Numeric:
                model.Name = req.Name;
                ((NumericCriterion)model).Max = req.Max ?? 0;
                ((NumericCriterion)model).Min = req.Min ?? 0;
                ((NumericCriterion)model).Unit = req.Unit ?? "";
                ((NumericCriterion)model).IsExclusive = req.IsExclusive ?? false;
                break;
            default:
                throw new Exception();
        }

        return model;
    }

    public CriterionDtoResponse ToResp(Criterion model)
    {
        return new CriterionDtoResponse()
        {
            Id = model.Id,
            Name = model.Name,
            Type = model.Type,
            Min = model.Type == CriterionType.Numeric ? ((NumericCriterion)model).Min : null,
            Max = model.Type == CriterionType.Numeric ? ((NumericCriterion)model).Max : null,
            Unit = model.Type == CriterionType.Numeric ? ((NumericCriterion)model).Unit : null,
            IsExclusive = model.Type == CriterionType.Numeric ? ((NumericCriterion)model).IsExclusive : null,
        };
    }

    public (PaginationMetadata, IEnumerable<CriterionDtoResponse>) ToPagedResponse(IPagedList<Criterion> models)
    {
        var metadata = IProfile<CriterionDtoRequest, Criterion, CriterionDtoResponse>.MapPaginationMetadata(models);
        return (metadata, models.Select(ToResp));
    }
}