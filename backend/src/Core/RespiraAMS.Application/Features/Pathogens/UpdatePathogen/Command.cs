using System.Text.Json.Serialization;
using RespiraAMS.Application.Abstracts.CQRS;

namespace RespiraAMS.Application.Features.Pathogens.UpdatePathogen;

public class UpdatePathogenCommand : ICommand
{
    [JsonIgnore] public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}