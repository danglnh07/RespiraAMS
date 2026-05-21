using FluentValidation;
using RespiraAMS.Core.Dtos;
using RespiraAMS.Core.Enums;

namespace RespiraAMS.Core.Validators;

public class CriterionValidator : AbstractValidator<CriterionDtoRequest>
{
    public CriterionValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required");
        RuleFor(x => x.Type)
            .IsInEnum()
            .WithMessage("Type is invalid");
        When(x => x.Type == CriterionType.Numeric, () =>
        {
            RuleFor(x => x.Max)
                .NotNull()
                .WithMessage("Max is required");
            RuleFor(x => x.Min)
                .NotNull()
                .WithMessage("Min is required");
            RuleFor(x => x)
                .Must(x => x.Min <= x.Max)
                .WithMessage("Min must be less than or equal to max");
        });
    }
}