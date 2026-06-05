using System.Text.Json.Serialization;
using RespiraAMS.Application.Abstracts.CQRS;
using RespiraAMS.Application.Shared.Dtos;

namespace RespiraAMS.Application.Features.ResistanceRiskFactors.CreateResistanceRiskFactor;

public class CreateResistanceRiskFactorCommand : ICommand
{
    [JsonIgnore]
    public Guid DiseaseId { get; set; }
    public Guid PathogenId  { get; set; }
    public CreateCriterionCommand Criterion { get; set; } = null!;
    public string Name { get; set; } = string.Empty;   
}

public class CreateResistanceRiskFactorResult(Guid id)
{
    public Guid Id { get; set; } = id;
}