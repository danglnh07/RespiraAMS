using System.Text.Json.Serialization;
using RespiraAMS.Application.Abstracts.CQRS;
using RespiraAMS.Application.Shared.Dtos;

namespace RespiraAMS.Application.Features.IcuHospitalizeCriteria.CreateIcuHospitalizeCriterion;

public class CreateIcuHospitalizeCriterionCommand : ICommand
{
    [JsonIgnore] public Guid DiseaseId { get; set; }
    public bool IsMainCriteria { get; set; }
    public CreateCriterionCommand Criterion { get; set; } = null!;
}

public class CreateIcuHospitalizeCriterionResult(Guid id)
{
    public Guid Id { get; set; } = id;
}