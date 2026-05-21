using RespiraAMS.Application.Dtos;
using RespiraAMS.Application.Profiles.Contracts;
using RespiraAMS.Domain.Models;
using X.PagedList;

namespace RespiraAMS.Application.Profiles.Implementations;

public class DiseaseProfile(
    IcuHospitalizeCriterionProfile icuProfile,
    ResistanceRiskFactorProfile infectionProfile,
    DiseasePathogenProfile diseasePathogenProfile) : IProfile<DiseaseDtoRequest, Disease, DiseaseDtoResponse>
{
    public Disease ToModel(DiseaseDtoRequest req)
    {
        return new Disease()
        {
            Name = req.Name,
            Description = req.Description,
            RequiredIcuMainCriteria = req.RequiredIcuMainCriteria,
            RequiredIcuSecondaryCriteria = req.RequiredIcuSecondaryCriteria,
        };
    }

    public Disease MapModel(DiseaseDtoRequest req, Disease model)
    {
        model.Name = req.Name;
        model.Description = req.Description;
        model.RequiredIcuMainCriteria = req.RequiredIcuMainCriteria;
        model.RequiredIcuSecondaryCriteria = req.RequiredIcuSecondaryCriteria;
        return model;
    }

    public DiseaseDtoResponse ToResp(Disease model)
    {
        return new DiseaseDtoResponse
        {
            Id = model.Id,
            Name = model.Name,
            Description = model.Description,
            RequiredIcuMainCriteria = model.RequiredIcuMainCriteria,
            RequiredIcuSecondaryCriteria = model.RequiredIcuSecondaryCriteria,
            IcuHospitalizedCriteria = model.IcuHospitalizeCriteria.Select(icuProfile.ToResp).ToList(),
            ResistanceRisks = model.ResistanceRisks.Select(infectionProfile.ToResp).ToList(),
            DiseasePathogens = model.DiseasePathogens.Select(diseasePathogenProfile.ToResp).ToList(),
        };
    }

    public (PaginationMetadata, IEnumerable<DiseaseDtoResponse>) ToPagedResponse(IPagedList<Disease> models)
    {
        var metadata = IProfile<DiseaseDtoRequest, Disease, DiseaseDtoResponse>.MapPaginationMetadata(models);
        return (metadata, models.Select(ToResp));
    }
}