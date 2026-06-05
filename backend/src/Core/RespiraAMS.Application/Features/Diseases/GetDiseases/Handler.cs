using Microsoft.EntityFrameworkCore;
using RespiraAMS.Application.Abstracts.CQRS;
using RespiraAMS.Application.Abstracts.Data;

namespace RespiraAMS.Application.Features.Diseases.GetDiseases;

public class GetDiseasesHandler(IDbContext context) : IQueryHandler<GetDiseasesQuery, IEnumerable<DiseaseItem>>
{
    public async Task<IEnumerable<DiseaseItem>> HandleAsync(GetDiseasesQuery query)
    {
        var diseases = await context.Diseases
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new DiseaseItem()
            {
                Id = x.Id,
                Name = x.Name,
            })
            .ToListAsync();
        return diseases;
    }
}