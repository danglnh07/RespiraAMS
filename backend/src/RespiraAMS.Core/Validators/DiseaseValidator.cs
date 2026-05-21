using FluentValidation;
using RespiraAMS.Core.Dtos;

namespace RespiraAMS.Core.Validators;

public class DiseaseValidator : AbstractValidator<DiseaseDtoRequest>
{
    public DiseaseValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required");
        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description is required");
        RuleFor(x => x.RequiredIcuMainCriteria)
            .GreaterThan(0)
            .WithMessage("Required ICU main criteria must be greater than 0");
        RuleFor(x => x.RequiredIcuSecondaryCriteria)
            .GreaterThan(0)
            .WithMessage("Required ICU secondary criteria must be greater than 0");
    }
}