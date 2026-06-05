using FluentValidation;
using RespiraAMS.Application.Features.Antibiotics.Shared;
using RespiraAMS.Application.Shared.Validations;

namespace RespiraAMS.Application.Features.Antibiotics.UpdateAntibiotic;

public class UpdateAntibioticValidator : AbstractValidator<UpdateAntibioticCommand>
{
    public UpdateAntibioticValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Antibiotic ID is required");
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Antibiotic name is required");
        RuleFor(x => x.AntibioticSpectrumId)
            .NotEmpty()
            .WithMessage("Antibiotic spectrum ID is required");
        RuleFor(x => x.Category)
            .IsInEnum()
            .WithMessage("Invalid value for antibiotic category");
        RuleFor(x => x.RouteOfAdministrations)
            .NotEmpty()
            .WithMessage("Route of administrations are required");
        RuleForEach(x => x.RouteOfAdministrations)
            .IsInEnum()
            .WithMessage("Invalid value for route of administrations");
        RuleFor(x => x.Dosages)
            .IsDosagesValid();
    }
}