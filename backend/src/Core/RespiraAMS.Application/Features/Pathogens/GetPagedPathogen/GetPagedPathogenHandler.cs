using BuildingBlocks.Dtos;
using RespiraAMS.Application.Abstracts.Data;
using RespiraAMS.Application.Mappers;
using X.PagedList.EF;

namespace RespiraAMS.Application.Features.Pathogens.GetPagedPathogen;

public class GetPagedPathogenHandler(IDbContext context, PathogenMapper mapper)
{
    public async Task<Pagination<GetPagedPathogenItem>> HandleAsync(GetPagedPathogenQuery query)
    {
        var pathogens = await context.Pathogens
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new GetPagedPathogenItem()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
            })
            .ToPagedListAsync(query.Param.Page, query.Param.Size);
        return mapper.ToPagination(pathogens);
    }
}