using System;
using FluentValidation;
using RespiraAMS.Application.Dtos;

namespace RespiraAMS.Application.Validators;

public class TreatmentProtocolValidator : AbstractValidator<TreatmentProtocolDtoRequest>
{
    public TreatmentProtocolValidator()
    {
        RuleFor(x => x.DiseaseId)
            .NotEqual(Guid.Empty)
            .WithMessage("Disease ID is required");
        RuleFor(x => x.Version)
            .GreaterThan(0)
            .WithMessage("Version must be  greater than 0");
        RuleFor(x => x.Severity)
            .IsInEnum()
            .WithMessage("Severity is invalid");
        RuleFor(x => x.TreatmentSite)
            .IsInEnum()
            .WithMessage("Treatment site is invalid");
        RuleFor(x => x.SpecialInfectionId)
            .Must(id => id != Guid.Empty)
            .WithMessage("Special infection ID must not equal default value");
        RuleForEach(x => x.OtherCriteriaIds)
            .NotEqual(Guid.Empty)
            .WithMessage("Other criteria ID must not equal to Guid empty");
        RuleFor(x => x.MedicineIds)
            .NotEmpty()
            .WithMessage("Medicine IDs must not be empty");
        RuleForEach(x => x.MedicineIds)
            .NotEqual(Guid.Empty)
            .WithMessage("All Medicine ID must not equal to Guid empty");
    }
}