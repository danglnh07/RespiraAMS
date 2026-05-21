using RespiraAMS.Core.Enums;

namespace RespiraAMS.Core.Models;

/// <summary>
/// Antibiotic class. Sometimes refer as medicine
/// </summary>
public class Antibiotic : Base
{
    public string Name { get; set; } = string.Empty;
    public Guid AntibioticSpectrumId { get; set; }
    public AntibioticSpectrum AntibioticSpectrum { get; set; } = null!;
    public AwareCategory Category { get; set; }
    public List<RouteOfAdministration> RouteOfAdministrations { get; set; } = [];
    public Dictionary<RouteOfAdministration, string> Dosages { get; set; } = [];
}