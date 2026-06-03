using FluentValidation;

namespace RespiraAMS.Application.Features.AntibioticSpectra.DeleteAntibioticSpectrum;

public class DeleteAntibioticSpectrumValidator : AbstractValidator<DeleteAntibioticSpectrumCommand>
{
    public DeleteAntibioticSpectrumValidator()
    {
        RuleFor(x => x.Id)
            .NotEqual(Guid.Empty)
            .WithMessage("Antibiotic spectrum ID is required");
    }
}