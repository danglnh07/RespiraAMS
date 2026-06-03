using FluentValidation;
using RespiraAMS.Application.Shared;

namespace RespiraAMS.Application.Features.AntibioticSpectra.UpdateAntibioticSpectrum;

public class UpdateAntibioticSpectrumValidator : AbstractValidator<UpdateAntibioticSpectrumCommand>
{
    public UpdateAntibioticSpectrumValidator()
    {
        RuleFor(x => x.Id).IsValidGuid("Antibiotic spectrum ID");
        RuleFor(x => x.Name).NotEmptyString("Antibiotic spectrum name");
        RuleFor(x => x.Description).NotEmptyString("Antibiotic spectrum description");
    }
}