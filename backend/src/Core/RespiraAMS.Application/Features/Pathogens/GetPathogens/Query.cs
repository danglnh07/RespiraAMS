using RespiraAMS.Application.Abstracts.CQRS;

namespace RespiraAMS.Application.Features.Pathogens.GetPathogens;

public class GetPathogensQuery : IQuery;

public class PathogenItem
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}