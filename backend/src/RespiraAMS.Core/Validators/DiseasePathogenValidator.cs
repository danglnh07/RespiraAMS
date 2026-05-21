using FluentValidation;
using RespiraAMS.Core.Dtos;

namespace RespiraAMS.Core.Validators;

public class DiseasePathogenValidator : AbstractValidator<DiseasePathogenDtoRequest>
{
    public DiseasePathogenValidator()
    {
        RuleFor(x => x.DiseaseId)
            .NotEqual(Guid.Empty)
            .WithMessage("Disease ID is required.");
        RuleFor(x => x.PathogenId)
            .NotEqual(Guid.Empty)
            .WithMessage("Pathogen ID is required.");
        RuleFor(x => x.Severity)
            .IsInEnum()
            .WithMessage("Severity is not valid");
        RuleFor(x => x.TreatmentSite)
            .IsInEnum()
            .WithMessage("Treatment site is not valid");
    }
}