using FluentValidation;
using RespiraAMS.Core.Dtos;

namespace RespiraAMS.Core.Validators;

public class AntibioticSpectrumValidator : AbstractValidator<AntibioticSpectrumDtoRequest>
{
    public AntibioticSpectrumValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Antibiotic spectrum name is required");
    }
}