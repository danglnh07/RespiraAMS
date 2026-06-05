using FluentValidation;
using RespiraAMS.Application.Shared.Validations;

namespace RespiraAMS.Application.Features.ResistanceRiskFactors.CreateResistanceRiskFactor;

public class CreateResistanceRiskFactorValidator : AbstractValidator<CreateResistanceRiskFactorCommand>
{
    public CreateResistanceRiskFactorValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Resistance risk factor name is required");
        RuleFor(x => x.DiseaseId)
            .NotEmpty()
            .WithMessage("Disease ID is required");
        RuleFor(x => x.PathogenId)
            .NotEmpty()
            .WithMessage("Pathogen ID is required");
        RuleFor(x => x.Criterion)
            .SetValidator(new CreateCriterionValidator());
    }
}