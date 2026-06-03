using FluentValidation;
using RespiraAMS.Application.Shared;

namespace RespiraAMS.Application.Features.AntibioticSpectra.CreateAntibioticSpectrum;

public class CreateAntibioticSpectrumValidator : AbstractValidator<CreateAntibioticSpectrumCommand>
{
    public CreateAntibioticSpectrumValidator()
    {
        RuleFor(x => x.Name).NotEmptyString("Antibiotic spectrum name");
        RuleFor(x => x.Description).NotEmptyString("Antibiotic spectrum description");
    }
}