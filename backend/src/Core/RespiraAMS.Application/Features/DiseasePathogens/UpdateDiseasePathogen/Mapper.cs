using RespiraAMS.Application.Abstracts.Mappers;
using RespiraAMS.Domain.Models;

namespace RespiraAMS.Application.Features.DiseasePathogens.UpdateDiseasePathogen;

public class UpdateDiseasePathogenMapper : IUpdateMapper<DiseasePathogen, UpdateDiseasePathogenCommand>
{
    public void MapModel(DiseasePathogen model, UpdateDiseasePathogenCommand command)
    {
        // Disease shouldn't be change
        model.PathogenId = command.PathogenId;
        model.Severity = command.Severity;
        model.TreatmentSite = command.TreatmentSite;
    }
}