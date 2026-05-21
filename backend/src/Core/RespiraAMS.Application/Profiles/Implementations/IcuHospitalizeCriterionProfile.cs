using RespiraAMS.Application.Dtos;
using RespiraAMS.Application.Profiles.Contracts;
using RespiraAMS.Domain.Models;
using X.PagedList;

namespace RespiraAMS.Application.Profiles.Implementations;

public class IcuHospitalizeCriterionProfile(CriterionProfile criterionProfile) : IProfile<IcuHospitalizedCriterionDtoRequest, IcuHospitalizeCriterion, IcuHospitalizedCriterionDtoResponse>
{
    public IcuHospitalizeCriterion ToModel(IcuHospitalizedCriterionDtoRequest req)
    {
        return new IcuHospitalizeCriterion()
        {
            DiseaseId = req.DiseaseId,
            Criterion = criterionProfile.ToModel(req.Criterion),
            IsMainCriteria = req.IsMainCriteria,
        };
    }

    public IcuHospitalizeCriterion MapModel(IcuHospitalizedCriterionDtoRequest req, IcuHospitalizeCriterion model)
    {
        model.Criterion = criterionProfile.MapModel(req.Criterion, model.Criterion);
        model.IsMainCriteria = req.IsMainCriteria;
        return model;
    }

    public IcuHospitalizedCriterionDtoResponse ToResp(IcuHospitalizeCriterion model)
    {
        return new IcuHospitalizedCriterionDtoResponse()
        {
            Id = model.Id,
            DiseaseId = model.DiseaseId,
            Criterion = criterionProfile.ToResp(model.Criterion),
            IsMainCriteria = model.IsMainCriteria,
        };
    }

    public (PaginationMetadata, IEnumerable<IcuHospitalizedCriterionDtoResponse>) ToPagedResponse(IPagedList<IcuHospitalizeCriterion> models)
    {
        var metadata =
            IProfile<IcuHospitalizedCriterionDtoRequest, IcuHospitalizeCriterion, IcuHospitalizedCriterionDtoResponse>
                .MapPaginationMetadata(models); 
        return (metadata, models.Select(ToResp));
    }
}