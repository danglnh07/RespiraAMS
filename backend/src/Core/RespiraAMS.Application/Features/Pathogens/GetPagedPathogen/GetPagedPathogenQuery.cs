using BuildingBlocks.Dtos;

namespace RespiraAMS.Application.Features.Pathogens.GetPagedPathogen;

public class GetPagedPathogenQuery
{
    public PaginationParam Param { get; set; } = null!;
}

public class GetPagedPathogenItem
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}