using System.Collections.Generic;
using System.Linq;
using RespiraAMS.Application.Dtos;
using RespiraAMS.Application.Profiles.Contracts;
using RespiraAMS.Domain.Models;
using X.PagedList;

namespace RespiraAMS.Application.Profiles.Implementations;

public class TreatmentProtocolProfile(
    PathogenProfile pathogenProfile,
    CriterionProfile criterionProfile,
    AntibioticProfile antibioticProfile)
    : IProfile<TreatmentProtocolDtoRequest, TreatmentProtocol, TreatmentProtocolDtoResponse>
{
    public TreatmentProtocol ToModel(TreatmentProtocolDtoRequest req)
    {
        return new TreatmentProtocol()
        {
            DiseaseId = req.DiseaseId,
            Version = req.Version,
            Severity = req.Severity,
            TreatmentSite = req.TreatmentSite,
            SpecialInfectionId = req.SpecialInfectionId,
            OtherCriteriaIds = req.OtherCriteriaIds,
            MedicineIds = req.MedicineIds,
        };
    }

    public TreatmentProtocol MapModel(TreatmentProtocolDtoRequest req, TreatmentProtocol model)
    {
        model.DiseaseId = req.DiseaseId;
        model.Version = req.Version;
        model.Severity = req.Severity;
        model.TreatmentSite = req.TreatmentSite;
        model.SpecialInfectionId = req.SpecialInfectionId;
        model.OtherCriteriaIds = req.OtherCriteriaIds;
        model.MedicineIds = req.MedicineIds;
        return model;
    }

    public TreatmentProtocolDtoResponse ToResp(TreatmentProtocol model)
    {
        return new TreatmentProtocolDtoResponse()
        {
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
            DiseaseId = model.DiseaseId,
            Version = model.Version,
            Severity = model.Severity,
            TreatmentSite = model.TreatmentSite,
            SpecialInfection = model.SpecialInfection is null ? null : pathogenProfile.ToResp(model.SpecialInfection),
            OtherCriteria = model.OtherCriteria.Select(criterionProfile.ToResp).ToList(),
            Medicines = model.Medicines.Select(antibioticProfile.ToResp).ToList(),
        };
    }

    public (PaginationMetadata, IEnumerable<TreatmentProtocolDtoResponse>) ToPagedResponse(
        IPagedList<TreatmentProtocol> models)
    {
        var metadata = IProfile<TreatmentProtocolDtoRequest, TreatmentProtocol, TreatmentProtocolDtoResponse>
            .MapPaginationMetadata(models);
        return (metadata, models.Select(ToResp));
    }
}