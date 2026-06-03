using BuildingBlocks.Dtos;
using Microsoft.Extensions.Logging;
using RespiraAMS.Application.Abstracts.Data;
using RespiraAMS.Application.Mappers;
using X.PagedList.EF;

namespace RespiraAMS.Application.Features.AntibioticSpectra.GetPagedAntibioticSpectrum;

public class GetPagedAntibioticSpectrumHandler(IDbContext context, AntibioticSpectrumMapper mapper)
{
    public async Task<Pagination<GetPagedAntibioticSpectrumItem>> HandleAsync(GetPagedAntibioticSpectrumQuery query)
    {
        // Get paged list of spectra
        var spectra = await context.AntibioticSpectra
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new GetPagedAntibioticSpectrumItem()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
            })
            .ToPagedListAsync(query.Param.Page, query.Param.Size);
        return mapper.ToPagination(spectra);
    }
}