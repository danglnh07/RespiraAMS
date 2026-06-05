using RespiraAMS.Application.Abstracts.CQRS;

namespace RespiraAMS.Application.Features.Antibiotics.GetAntibiotics;

public class GetAntibioticsQuery : IQuery;

public class AntibioticItem
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}