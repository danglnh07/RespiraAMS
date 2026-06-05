using System.Text.Json.Serialization;
using RespiraAMS.Application.Abstracts.CQRS;
using RespiraAMS.Application.Shared.Dtos;

namespace RespiraAMS.Application.Features.IcuHospitalizeCriteria.UpdateIcuHospitalizeCriterion;

public class UpdateIcuHospitalizeCriterionCommand : ICommand
{
    [JsonIgnore]
    public Guid Id { get; set; }
    public bool IsMainCriteria { get; set; }
    public UpdateCriterionCommand Criterion { get; set; } = null!;
}