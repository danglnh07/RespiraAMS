namespace RespiraAMS.Application.Features.AntibioticSpectra.GetPagedAntibioticSpectrum;

public class GetPagedAntibioticSpectrumQuery
{
    public int Page { get; set; }
    public int Size { get; set; }
}

public class GetPagedAntibioticSpectrumItem
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}