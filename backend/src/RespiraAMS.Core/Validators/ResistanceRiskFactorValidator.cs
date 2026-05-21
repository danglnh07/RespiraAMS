using FluentValidation;
using RespiraAMS.Core.Dtos;

namespace RespiraAMS.Core.Validators;

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
            .SetValidator(new CriterionValidator());
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required");
    }
}