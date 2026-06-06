using System.Text.Json.Serialization;
using RespiraAMS.Application.Abstracts.CQRS;
using RespiraAMS.Domain.Enums;

namespace RespiraAMS.Application.Features.Antibiotics.UpdateAntibiotic;

public class UpdateAntibioticCommand : ICommand
{
    [JsonIgnore] public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid AntibioticSpectrumId { get; set; }
    public AwareCategory Category { get; set; }
    public Dictionary<RouteOfAdministration, List<string>> Dosages { get; set; } = [];
}