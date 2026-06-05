using BuildingBlocks.Dtos;
using RespiraAMS.Application.Abstracts.CQRS;

namespace RespiraAMS.Application.Features.Diseases.GetPagedDiseases;

public class GetPagedDiseasesQuery : IQuery
{
    public PaginationParam Param { get; set; } = null!;
}

public class DiseaseItem
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}