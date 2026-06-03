namespace RespiraAMS.Application.Features.AntibioticSpectra.CreateAntibioticSpectrum;

public class CreateAntibioticSpectrumCommand
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

public class CreateAntibioticSpectrumResult(Guid id)
{
    public Guid Id { get; set; } = id;
}