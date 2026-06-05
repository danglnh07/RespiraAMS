using Microsoft.EntityFrameworkCore;
using RespiraAMS.Application.Abstracts.CQRS;
using RespiraAMS.Application.Abstracts.Data;

namespace RespiraAMS.Application.Features.AntibioticSpectra.GetAntibioticSpectra;

public class GetAntibioticSpectraHandler(IDbContext context) 
    : IQueryHandler<GetAntibioticSpectraQuery, IEnumerable<AntibioticSpectrumItem>>
{
    public async Task<IEnumerable<AntibioticSpectrumItem>> HandleAsync(GetAntibioticSpectraQuery query)
    {
        // Get paged list of spectra
        var spectra = await context.AntibioticSpectra
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new AntibioticSpectrumItem()
            {
                Id = x.Id,
                Name = x.Name,
            })
            .ToListAsync();
        return spectra;
    }
}