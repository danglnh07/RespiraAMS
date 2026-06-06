using System.Text.Json.Serialization;
using RespiraAMS.Application.Abstracts.CQRS;
using RespiraAMS.Application.Shared.Dtos;

namespace RespiraAMS.Application.Features.TreatmentProtocols.AddNewCriteria;

public class AddNewCriteriaCommand : ICommand
{
    [JsonIgnore] public Guid Id { get; set; }
    public List<CreateCriterionCommand> Criteria { get; set; } = [];
}

