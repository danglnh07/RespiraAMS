using FluentValidation;
using RespiraAMS.Application.Dtos;

namespace RespiraAMS.Application.Validators;

public class AntibioticSpectrumValidator : AbstractValidator<AntibioticSpectrumDtoRequest>
{
    public AntibioticSpectrumValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Antibiotic spectrum name is required");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Antibiotic spectrum description is required");
    }
}