using BuildingBlocks.Dtos;
using Microsoft.EntityFrameworkCore;
using RespiraAMS.Application.Abstracts.CQRS;
using RespiraAMS.Application.Abstracts.Data;
using RespiraAMS.Application.Shared.Mappers;
using X.PagedList.EF;

namespace RespiraAMS.Application.Features.Pathogens.GetPagedPathogens;

public class GetPagedPathogensHandler(IDbContext context)
    : IQueryHandler<GetPagedPathogensQuery, Pagination<PathogenItem>>
{
    public async Task<Pagination<PathogenItem>> HandleAsync(GetPagedPathogensQuery query)
    {
        var queryable = context.Pathogens.AsQueryable();
        if (query.Filter is not null)
        {
            if (query.Filter.Name is not null)
            {
                queryable = queryable
                    .Where(x => EF.Functions.ILike(x.Name, $"%{query.Filter.Name}%"));
            }
        }

        var pathogens = await queryable
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new PathogenItem()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
            })
            .ToPagedListAsync(query.Param.Page, query.Param.Size);
        return MapperBase.ToPagination(pathogens);
    }
}