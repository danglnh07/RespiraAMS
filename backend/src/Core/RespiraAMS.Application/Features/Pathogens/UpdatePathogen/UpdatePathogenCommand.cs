using System.Text.Json.Serialization;

namespace RespiraAMS.Application.Features.Pathogens.UpdatePathogen;

public class UpdatePathogenCommand
{
    [JsonIgnore]
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

public class UpdatePathogenResult
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}