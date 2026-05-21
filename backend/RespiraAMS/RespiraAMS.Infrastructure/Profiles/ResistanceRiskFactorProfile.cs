using RespiraAMS.Core.Dtos;
using RespiraAMS.Core.Models;
using RespiraAMS.Core.Profiles;
using X.PagedList;

namespace RespiraAMS.Infrastructure.Profiles;

public class ResistanceRiskFactorProfile(CriterionProfile criterionProfile, PathogenProfile pathogenProfile)
    : IProfile<ResistanceRiskFactorDtoRequest,
        ResistanceRiskFactor, ResistanceRiskFactorDtoResponse>
{
    public ResistanceRiskFactor ToModel(ResistanceRiskFactorDtoRequest req)
    {
        return new ResistanceRiskFactor()
        {
            DiseaseId = req.DiseaseId,
            PathogenId = req.PathogenId,
            Criterion = criterionProfile.ToModel(req.Criterion),
            Name = req.Name,
        };
    }

    public ResistanceRiskFactor MapModel(ResistanceRiskFactorDtoRequest req,
        ResistanceRiskFactor model)
    {
        model.Name = req.Name;
        model.PathogenId = req.PathogenId;
        model.Criterion = criterionProfile.MapModel(req.Criterion, model.Criterion);
        return model;
    }

    public ResistanceRiskFactorDtoResponse ToResp(ResistanceRiskFactor model)
    {
        return new ResistanceRiskFactorDtoResponse()
        {
            Id = model.Id,
            DiseaseId = model.DiseaseId,
            Name = model.Name,
            Pathogen = pathogenProfile.ToResp(model.Pathogen),
            Criterion = criterionProfile.ToResp(model.Criterion),
        };
    }

    public (PaginationMetadata, IEnumerable<ResistanceRiskFactorDtoResponse>) ToPagedResponse(
        IPagedList<ResistanceRiskFactor> models)
    {
        var metadata =
            IProfile<ResistanceRiskFactorDtoRequest, ResistanceRiskFactor,
                    ResistanceRiskFactorDtoResponse>
                .MapPaginationMetadata(models);
        return (metadata, models.Select(ToResp));
    }
}