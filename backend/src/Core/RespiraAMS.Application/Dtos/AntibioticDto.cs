using System.ComponentModel;
using RespiraAMS.Domain.Enums;

namespace RespiraAMS.Application.Dtos;

public class AntibioticDtoRequest
{
    [Description("Antibiotic name")]
    public string Name { get; set; } = string.Empty;
    [Description("Antibiotic spectrum ID")]
    public Guid AntibioticSpectrumId { get; set; }
    [Description("Antibiotic category (AWaRe metrics). The value can either be number or string (case insensitive) value")]
    public AwareCategory Category { get; set; }
    [Description("Route of Administration. The value can either be number or string (case insensitive) value)")]
    public List<RouteOfAdministration> RouteOfAdministrations { get; set; } = [];
    [Description("Medicine dosages, in the form of \"RouteOfAdministration\": \"List<string>\"")]
    public Dictionary<RouteOfAdministration, List<string>> Dosages { get; set; } = [];
}

public class AntibioticDtoResponse
{
    [Description("Antibiotic ID")]
    public Guid Id { get; set; }
    [Description("Antibiotic name")]
    public string Name { get; set; } = string.Empty;
    [Description("Antibiotic spectrum")]
    public AntibioticSpectrumDtoResponse AntibioticSpectrum { get; set; } = null!;
    [Description("Antibiotic category (AWaRe metrics)")]
    public AwareCategory Category { get; set; }
    [Description("Route of Administration")]
    public List<RouteOfAdministration> RouteOfAdministrations { get; set; } = [];
    [Description("Medicine dosages")]
    public Dictionary<RouteOfAdministration, List<string>> Dosages { get; set; } = [];
}