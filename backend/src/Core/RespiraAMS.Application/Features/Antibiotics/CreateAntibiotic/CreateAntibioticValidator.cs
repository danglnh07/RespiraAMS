using FluentValidation;
using RespiraAMS.Application.Features.Antibiotics.Shared;
using RespiraAMS.Application.Shared;

namespace RespiraAMS.Application.Features.Antibiotics.CreateAntibiotic;

public class CreateAntibioticValidator : AbstractValidator<CreateAntibioticCommand>
{
    public CreateAntibioticValidator()
    {
        RuleFor(x => x.Name).NotEmptyString("Antibiotic name");
        RuleFor(x => x.AntibioticSpectrumId).IsValidGuid("Antibiotic spectrum ID");
        RuleFor(x => x.Category).IsEnum("Antibiotic category");
        RuleFor(x => x.RouteOfAdministrations)
            .IsEnumList("Antibiotic route of administrations");
        RuleFor(x => x.Dosages).IsDosagesValid();
    }
}