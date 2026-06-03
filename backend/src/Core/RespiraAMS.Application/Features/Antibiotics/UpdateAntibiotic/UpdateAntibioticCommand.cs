using System.Text.Json.Serialization;
using RespiraAMS.Domain.Enums;

namespace RespiraAMS.Application.Features.Antibiotics.UpdateAntibiotic;

public class UpdateAntibioticCommand
{
    [JsonIgnore] public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid AntibioticSpectrumId { get; set; }
    public AwareCategory Category { get; set; }
    public List<RouteOfAdministration> RouteOfAdministrations { get; set; } = [];
    public Dictionary<RouteOfAdministration, List<string>> Dosages { get; set; } = [];
}

public class UpdateAntibioticResult
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid AntibioticSpectrumId { get; set; }
    public AwareCategory Category { get; set; }
    public List<RouteOfAdministration> RouteOfAdministrations { get; set; } = [];
    public Dictionary<RouteOfAdministration, List<string>> Dosages { get; set; } = [];
}