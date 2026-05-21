using RespiraAMS.Core.Dtos;
using RespiraAMS.Core.Models;
using RespiraAMS.Core.Profiles;
using X.PagedList;

namespace RespiraAMS.Infrastructure.Profiles;

public class DiseasePathogenProfile(PathogenProfile pathogenProfile) : IProfile<DiseasePathogenDtoRequest, DiseasePathogen, DiseasePathogenDtoResponse>
{
    public DiseasePathogen ToModel(DiseasePathogenDtoRequest req)
    {
        return new DiseasePathogen()
        {
            DiseaseId = req.DiseaseId,
            PathogenId = req.PathogenId,
            Severity = req.Severity,
            TreatmentSite = req.TreatmentSite,
        };
    }

    public DiseasePathogen MapModel(DiseasePathogenDtoRequest req, DiseasePathogen model)
    {
        model.DiseaseId = req.DiseaseId;
        model.PathogenId = req.PathogenId;
        model.Severity = req.Severity;
        model.TreatmentSite = req.TreatmentSite;
        return model;
    }

    public DiseasePathogenDtoResponse ToResp(DiseasePathogen model)
    {
        return new DiseasePathogenDtoResponse()
        {
            Id = model.Id,
            Pathogen = pathogenProfile.ToResp(model.Pathogen),
            Severity = model.Severity,
            TreatmentSite = model.TreatmentSite,
        };
    }

    public (PaginationMetadata, IEnumerable<DiseasePathogenDtoResponse>) ToPagedResponse(IPagedList<DiseasePathogen> models)
    {
        var metadata = IProfile<DiseasePathogenDtoRequest, DiseasePathogen, DiseasePathogenDtoResponse>.MapPaginationMetadata(models);
        return (metadata, models.Select(ToResp));
    }
}