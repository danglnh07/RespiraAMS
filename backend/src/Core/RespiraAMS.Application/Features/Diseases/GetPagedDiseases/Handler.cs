using BuildingBlocks.Dtos;
using RespiraAMS.Application.Abstracts.CQRS;
using RespiraAMS.Application.Abstracts.Data;
using RespiraAMS.Application.Shared.Mappers;
using X.PagedList.EF;

namespace RespiraAMS.Application.Features.Diseases.GetPagedDiseases;

public class GetPagedDiseasesHandler(IDbContext context)
    : IQueryHandler<GetPagedDiseasesQuery, Pagination<DiseaseItem>>
{
    public async Task<Pagination<DiseaseItem>> HandleAsync(GetPagedDiseasesQuery query)
    {
        var diseases = await context.Diseases
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new DiseaseItem()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
            })
            .ToPagedListAsync(query.Param.Page, query.Param.Size);
        return MapperBase.ToPagination(diseases);
    }
}