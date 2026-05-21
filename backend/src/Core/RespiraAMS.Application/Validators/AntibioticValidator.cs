using FluentValidation;
using RespiraAMS.Application.Dtos;

namespace RespiraAMS.Application.Validators;

public class AntibioticValidator : AbstractValidator<AntibioticDtoRequest>
{
    public AntibioticValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Antibiotic name is required");
        RuleFor(x => x.AntibioticSpectrumId).NotEqual(Guid.Empty).WithMessage("Antibiotic spectrum id is required");
        RuleFor(x => x.Category).IsInEnum().WithMessage("Antibiotic AWaRe category is invalid");
        RuleForEach(x => x.RouteOfAdministrations).IsInEnum().WithMessage("Route of administrations are invalid");
        RuleForEach(x => x.Dosages.Keys).IsInEnum().WithMessage("Dosages are invalid");
        RuleForEach(x => x.Dosages.Values).NotEmpty().WithMessage("Dosages are invalid");
    }
}