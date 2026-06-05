using Microsoft.EntityFrameworkCore;
using RespiraAMS.Application.Abstracts.CQRS;
using RespiraAMS.Application.Abstracts.Data;

namespace RespiraAMS.Application.Features.Pathogens.GetPathogens;

public class GetPathogensHandler(IDbContext context) : IQueryHandler<GetPathogensQuery, IEnumerable<PathogenItem>>
{
    public async Task<IEnumerable<PathogenItem>> HandleAsync(GetPathogensQuery query)
    {
        var pathogens = await context.Pathogens
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new PathogenItem()
            {
                Id = x.Id,
                Name = x.Name,
            })
            .ToListAsync();
        return pathogens;
    }
}