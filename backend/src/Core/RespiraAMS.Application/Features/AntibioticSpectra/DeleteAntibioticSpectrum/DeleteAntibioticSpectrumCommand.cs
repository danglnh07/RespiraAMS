namespace RespiraAMS.Application.Features.AntibioticSpectra.DeleteAntibioticSpectrum;

public class DeleteAntibioticSpectrumCommand(Guid id)
{
    public Guid Id { get; set; } = id;
}