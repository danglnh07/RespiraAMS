using FluentValidation;
using RespiraAMS.Application.Shared.Dtos;
using RespiraAMS.Domain.Enums;

namespace RespiraAMS.Application.Shared.Validations;

public class CreateCriterionValidator : AbstractValidator<CreateCriterionCommand>
{
    public CreateCriterionValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Criterion name is required");
        RuleFor(c => c.Type)
            .NotEmpty().WithMessage("Invalid value for criterion type");
        When(x => x.Type == CriterionType.Numeric, () =>
        {
            RuleFor(x => x.Max)
                .NotNull()
                .WithMessage("Criterion max value is required");
            RuleFor(x => x.Min)
                .NotNull()
                .WithMessage("Criterion min value is required");
            RuleFor(x => x)
                .Must(x => x.Min <= x.Max)
                .WithMessage("Criterion min value must be less than or equal to max");
            // It's allow to be empty, but not null
            RuleFor(x => x.Unit)
                .NotNull()
                .WithMessage("Criterion unit is required");
            RuleFor(x => x.IsExclusive)
                .NotNull()
                .WithMessage("Criterion is exclusive is required");
        });
        // Unit can still be empty, and is exclusive is a boolean, which has nothing to check 
    }
}

public class UpdateCriterionValidator : AbstractValidator<UpdateCriterionCommand>
{
    public UpdateCriterionValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Criterion name is required");
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
            // It's allow to be empty, but not null
            RuleFor(x => x.Unit)
                .NotNull()
                .WithMessage("Criterion unit is required");
            RuleFor(x => x.IsExclusive)
                .NotNull()
                .WithMessage("Criterion is exclusive is required");
        });
        // Unit can still be empty, and is exclusive is a boolean, which has nothing to check 
    }
}