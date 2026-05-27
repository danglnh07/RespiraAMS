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
            if (!Enum.IsDefined(dosage.Key))
            {
                return false;
            }
            
            // Validate values
            var value =  dosage.Value;
            if (value.Count == 0)
            {
                return false;
            }

            if (value.Any(string.IsNullOrWhiteSpace))
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
        RuleFor(x => x.Dosages).Must(ValidateDosage).WithMessage("Dosages are invalid");
    }
}