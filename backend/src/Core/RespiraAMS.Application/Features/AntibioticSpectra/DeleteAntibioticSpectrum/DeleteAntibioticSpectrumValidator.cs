using FluentValidation;
using RespiraAMS.Application.Shared;

namespace RespiraAMS.Application.Features.AntibioticSpectra.DeleteAntibioticSpectrum;

public class DeleteAntibioticSpectrumValidator : AbstractValidator<DeleteAntibioticSpectrumCommand>
{
    public DeleteAntibioticSpectrumValidator()
    {
        RuleFor(x => x.Id).IsValidGuid("Antibiotic spectrum ID");
    }
}