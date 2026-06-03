using RespiraAMS.Domain.Enums;

namespace RespiraAMS.Application.Features.Antibiotics.CreateAntibiotic;

public class CreateAntibioticCommand
{
    public string Name { get; set; } = string.Empty;
    public Guid AntibioticSpectrumId { get; set; }
    public AwareCategory Category { get; set; }
    public List<RouteOfAdministration> RouteOfAdministrations { get; set; } = [];
    public Dictionary<RouteOfAdministration, List<string>> Dosages { get; set; } = [];
}

public class CreateAntibioticResult(Guid id)
{
    public Guid Id { get; set; } = id;
}