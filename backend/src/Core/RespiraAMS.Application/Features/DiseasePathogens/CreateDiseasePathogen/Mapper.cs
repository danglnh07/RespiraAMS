using RespiraAMS.Application.Abstracts.Mappers;
using RespiraAMS.Domain.Models;

namespace RespiraAMS.Application.Features.DiseasePathogens.CreateDiseasePathogen;

public class CreateDiseasePathogenMapper : ICreateMapper<DiseasePathogen, CreateDiseasePathogenCommand>
{
    public DiseasePathogen ToModel(CreateDiseasePathogenCommand command)
    {
        return new DiseasePathogen()
        {
            DiseaseId = command.DiseaseId,
            PathogenId = command.PathogenId,
            Severity = command.Severity,
            TreatmentSite = command.TreatmentSite
        };
    }
}