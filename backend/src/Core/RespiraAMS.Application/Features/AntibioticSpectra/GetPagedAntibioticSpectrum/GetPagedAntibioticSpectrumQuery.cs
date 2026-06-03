using BuildingBlocks.Dtos;

namespace RespiraAMS.Application.Features.AntibioticSpectra.GetPagedAntibioticSpectrum;

public class GetPagedAntibioticSpectrumQuery(PaginationParam param)
{
    public PaginationParam Param { get; set; } = param;
}

public class GetPagedAntibioticSpectrumItem
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}