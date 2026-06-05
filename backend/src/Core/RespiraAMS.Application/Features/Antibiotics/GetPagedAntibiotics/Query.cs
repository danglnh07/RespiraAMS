using BuildingBlocks.Dtos;
using RespiraAMS.Application.Abstracts.CQRS;
using RespiraAMS.Domain.Enums;

namespace RespiraAMS.Application.Features.Antibiotics.GetPagedAntibiotics;

public class AntibioticFilter
{
    public string? Name { get; set; }
    public Guid? AntibioticSpectrumId { get; set; }
    public AwareCategory? Category { get; set; }
}

public class GetPagedAntibioticsQuery : IQuery
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

public class AntibioticItem
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public AntibioticSpectrumItem AntibioticSpectrum { get; set; } = null!;
    public AwareCategory Category { get; set; }
    public List<RouteOfAdministration> RouteOfAdministrations { get; set; } = [];
    public Dictionary<RouteOfAdministration, List<string>> Dosages { get; set; } = [];
}