using System.Text.Json.Serialization;
using RespiraAMS.Application.Abstracts.CQRS;
using RespiraAMS.Application.Shared.Dtos;

namespace RespiraAMS.Application.Features.ResistanceRiskFactors.UpdateResistanceRiskFactor;

public class UpdateResistanceRiskFactorCommand : ICommand
{
    [JsonIgnore]
    public Guid Id { get; set; }
    public Guid PathogenId  { get; set; }
    public UpdateCriterionCommand Criterion { get; set; } = null!;
    public string Name { get; set; } = string.Empty;   
}