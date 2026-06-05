using Microsoft.EntityFrameworkCore;
using RespiraAMS.Application.Abstracts.CQRS;
using RespiraAMS.Application.Abstracts.Data;

namespace RespiraAMS.Application.Features.Antibiotics.GetAntibiotics;

public class GetAntibioticsHandler(IDbContext context) : IQueryHandler<GetAntibioticsQuery, IEnumerable<AntibioticItem>>
{
    public async Task<IEnumerable<AntibioticItem>> HandleAsync(GetAntibioticsQuery query)
    {
        var antibiotics = await context.Antibiotics
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new AntibioticItem()
            {
                Id = x.Id,
                Name = x.Name,
            })
            .ToListAsync();
        return antibiotics;
    }
}