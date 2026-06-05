using System.Text.Json.Serialization;
using RespiraAMS.Application.Abstracts.CQRS;
using RespiraAMS.Domain.Enums;

namespace RespiraAMS.Application.Features.DiseasePathogens.UpdateDiseasePathogen;

public class UpdateDiseasePathogenCommand : ICommand
{
    [JsonIgnore] public Guid Id { get; set; }
    public Guid PathogenId { get; set; }
    public Severity Severity { get; set; }
    public TreatmentSite TreatmentSite { get; set; }
}