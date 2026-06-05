using RespiraAMS.Application.Abstracts.CQRS;

namespace RespiraAMS.Application.Features.AntibioticSpectra.DeleteAntibioticSpectrum;

public class DeleteAntibioticSpectrumCommand(Guid id) : ICommand
{
    public Guid Id { get; set; } = id;
}