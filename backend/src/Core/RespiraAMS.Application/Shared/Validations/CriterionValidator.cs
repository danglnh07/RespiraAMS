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
            .IsInEnum().WithMessage("Invalid value for criterion type");
        
        // When type is numeric, the specific properties of NumericCriterion 
        // must exists and valid
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
        
        // When type is Boolean, all NumericCriterion properties must not exists
        When(x => x.Type == CriterionType.Boolean, () =>
        {
            RuleFor(x => x.Min)
                .Must(x => x == null)
                .WithMessage("Criterion type is set to boolean, cannot accept value for Min");
            RuleFor(x => x.Max)
                .Must(x => x == null)
                .WithMessage("Criterion type is set to boolean, cannot accept value for Max");
            RuleFor(x => x.Unit)
                .Must(x => x == null)
                .WithMessage("Criterion type is set to boolean, cannot accept value for Unit");
            RuleFor(x => x.IsExclusive)
                .Must(x => x == null)
                .WithMessage("Criterion type is set to boolean, cannot accept value for IsExclusive");
        });
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
        When(x => x.Type == CriterionType.Boolean, () =>
        {
            RuleFor(x => x.Min)
                .Must(x => x == null)
                .WithMessage("Criterion type is set to boolean, cannot accept value for Min");
            RuleFor(x => x.Max)
                .Must(x => x == null)
                .WithMessage("Criterion type is set to boolean, cannot accept value for Max");
            RuleFor(x => x.Unit)
                .Must(x => x == null)
                .WithMessage("Criterion type is set to boolean, cannot accept value for Unit");
            RuleFor(x => x.IsExclusive)
                .Must(x => x == null)
                .WithMessage("Criterion type is set to boolean, cannot accept value for IsExclusive");
        });
    }
}