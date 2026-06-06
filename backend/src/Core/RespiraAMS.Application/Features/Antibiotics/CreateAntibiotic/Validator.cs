using FluentValidation;
using RespiraAMS.Application.Features.Antibiotics.Shared;

namespace RespiraAMS.Application.Features.Antibiotics.CreateAntibiotic;

public class CreateAntibioticValidator : AbstractValidator<CreateAntibioticCommand>
{
    public CreateAntibioticValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Antibiotic name is required");
        RuleFor(x => x.AntibioticSpectrumId)
            .NotEmpty()
            .WithMessage("Antibiotic spectrum ID is required");
        RuleFor(x => x.Category)
            .IsInEnum()
            .WithMessage("Invalid value for antibiotic category");
        RuleFor(x => x.Dosages).IsDosagesValid();
    }
}