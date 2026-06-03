using BuildingBlocks.Dtos;
using RespiraAMS.Domain.Enums;

namespace RespiraAMS.Application.Features.Antibiotics.GetPagedAntibiotic;

public class AntibioticFilter
{
    public Guid? AntibioticSpectrumId { get; set; }
    public AwareCategory? Category { get; set; }
}

public class GetPagedAntibioticQuery
{
    public PaginationParam Param { get; set; } = null!;
    public AntibioticFilter? Filter { get; set; }
}

public class AntibioticSpectrumItem
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

public class GetPagedAntibioticItem
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public AntibioticSpectrumItem AntibioticSpectrum { get; set; } = null!;
    public AwareCategory Category { get; set; }
    public List<RouteOfAdministration> RouteOfAdministrations { get; set; } = [];
    public Dictionary<RouteOfAdministration, List<string>> Dosages { get; set; } = [];
}