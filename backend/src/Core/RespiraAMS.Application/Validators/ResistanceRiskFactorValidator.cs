using FluentValidation;
using RespiraAMS.Application.Dtos;

namespace RespiraAMS.Application.Validators;

public class ResistanceRiskFactorValidator : AbstractValidator<ResistanceRiskFactorDtoRequest>
{
    public ResistanceRiskFactorValidator()
    {
        RuleFor(x => x.DiseaseId)
            .NotEqual(Guid.Empty)
            .WithMessage("Disease ID is required");
        RuleFor(x => x.PathogenId)
            .NotEqual(Guid.Empty)
            .WithMessage("Pathogen ID is required");
        RuleFor(x => x.Criterion)
            .SetValidator(new Application.Validators.CriterionValidator());
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required");
    }
}