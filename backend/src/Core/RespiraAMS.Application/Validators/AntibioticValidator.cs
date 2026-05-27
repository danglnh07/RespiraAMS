using System;
using FluentValidation;
using RespiraAMS.Application.Dtos;
using RespiraAMS.Domain.Enums;

namespace RespiraAMS.Application.Validators;

public class AntibioticValidator : AbstractValidator<AntibioticDtoRequest>
{
    private bool ValidateDosage(Dictionary<RouteOfAdministration, List<string>> dosages)
    {
        if (dosages.Count == 0)
        {
            return false;
        }

        foreach (var dosage in dosages)
        {
            // Validate key
            if (Enum.IsDefined(dosage.Key))
            {
                return false;
            }
            
            // Validate values
            var value =  dosage.Value;
            if (value.Count == 0)
            {
                return false;
            }

            if (value.All(d => !string.IsNullOrWhiteSpace(d)))
            {
                return false;
            }
        }

        return true;
    }
    
    public AntibioticValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Antibiotic name is required");
        RuleFor(x => x.AntibioticSpectrumId).NotEqual(Guid.Empty).WithMessage("Antibiotic spectrum id is required");
        RuleFor(x => x.Category).IsInEnum().WithMessage("Antibiotic AWaRe category is invalid");
        RuleForEach(x => x.RouteOfAdministrations).IsInEnum().WithMessage("Route of administrations are invalid");
        RuleFor(x => x.Dosages).NotEmpty().WithMessage("Dosages is required");
        // RuleForEach(x => x.Dosages.Keys).IsInEnum().WithMessage("Dosages are invalid");
        // RuleForEach(x => x.Dosages.Values)
        //     .NotEmpty()
        //     .Must(dosage => dosage.All(x => !string.IsNullOrWhiteSpace(x)))
        //     .WithMessage("Dosages are invalid");
        // RuleForEach(x => x.Dosages)
        //     .ChildRules(d =>
        //     {
        //         // Check if the list of is empty
        //         d.RuleFor(x => x.Value)
        //             .NotEmpty()
        //             .WithMessage("At least 1 dosage is required");
        //         // Check if all items in the list is empty string or not
        //         d.RuleFor(x => x.Value)
        //             .Must(values => values.All(v => !string.IsNullOrWhiteSpace(v)))
        //             .WithMessage("Dosages are invalid");
        //     });
        // RuleFor(x => x.Dosages)
        //     .NotEmpty().WithMessage("Dosages is required")
        //     .Must(dosages => dosages.Keys.All(k => Enum.IsDefined(typeof(RouteOfAdministration), k)))
        //     .WithMessage("Dosage route is invalid")
        //     .Must(dosages => dosages.Values.All(v => v.Count > 0))
        //     .WithMessage("Each route must have at least 1 dosage")
        //     .Must(dosages => dosages.Values.All(v => v.All(d => !string.IsNullOrWhiteSpace(d))))
        //     .WithMessage("Dosage values cannot be empty or whitespace");
        RuleFor(x => x.Dosages).Must(ValidateDosage).WithMessage("Dosages are invalid");
    }
}