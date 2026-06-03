using BuildingBlocks.Dtos;
using Microsoft.EntityFrameworkCore;
using RespiraAMS.Application.Abstracts.Data;
using RespiraAMS.Application.Mappers;
using X.PagedList.EF;

namespace RespiraAMS.Application.Features.Antibiotics.GetPagedAntibiotic;

public class GetPagedAntibioticHandler(IDbContext context, AntibioticMapper mapper)
{
    public async Task<Pagination<GetPagedAntibioticItem>> HandleAsync(GetPagedAntibioticQuery query)
    {
        // Construct filter
        var queryable = context.Antibiotics.AsQueryable();
        if (query.Filter is not null)
        {
            if (query.Filter.AntibioticSpectrumId is not null)
            {
                queryable = queryable.Where(x => x.AntibioticSpectrumId == query.Filter.AntibioticSpectrumId);
            }

            if (query.Filter.Category is not null)
            {
                queryable = queryable.Where(x => x.Category == query.Filter.Category);
            }
        }

        var antibiotics = await queryable
            .OrderByDescending(x => x.CreatedAt)
            .Include(x => x.AntibioticSpectrum)
            .Select(x => new GetPagedAntibioticItem()
            {
                Id = x.Id,
                AntibioticSpectrum = new AntibioticSpectrumItem()
                {
                    Id = x.AntibioticSpectrum.Id,
                    Name = x.AntibioticSpectrum.Name,
                    Description = x.AntibioticSpectrum.Description,
                },
                Category = x.Category,
                RouteOfAdministrations = x.RouteOfAdministrations,
                Dosages = x.Dosages,
            })
            .ToPagedListAsync(query.Param.Page, query.Param.Size);
        return mapper.ToPagination(antibiotics);
    }
}