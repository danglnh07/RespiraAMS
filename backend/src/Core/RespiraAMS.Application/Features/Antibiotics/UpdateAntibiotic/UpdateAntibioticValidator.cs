using FluentValidation;
using RespiraAMS.Application.Features.Antibiotics.Shared;
using RespiraAMS.Application.Shared;
using RespiraAMS.Domain.Enums;

namespace RespiraAMS.Application.Features.Antibiotics.UpdateAntibiotic;

public class UpdateAntibioticValidator : AbstractValidator<UpdateAntibioticCommand>
{
    public UpdateAntibioticValidator()
    {
        RuleFor(x => x.Id).IsValidGuid("Antibiotic ID");
        RuleFor(x => x.Name).NotEmptyString("Antibiotic name");
        RuleFor(x => x.AntibioticSpectrumId).IsValidGuid("Antibiotic spectrum ID");
        RuleFor(x => x.Category).IsEnum("Antibiotic category");
        RuleFor(x => x.RouteOfAdministrations)
            .IsEnumList("Antibiotic route of administrations");
        RuleFor(x => x.Dosages).IsDosagesValid();
    }
}