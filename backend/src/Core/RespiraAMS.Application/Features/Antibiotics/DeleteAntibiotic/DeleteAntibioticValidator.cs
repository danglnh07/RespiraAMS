using FluentValidation;
using RespiraAMS.Application.Shared;

namespace RespiraAMS.Application.Features.Antibiotics.DeleteAntibiotic;

public class DeleteAntibioticValidator : AbstractValidator<DeleteAntibioticCommand>
{
    public DeleteAntibioticValidator()
    {
        RuleFor(x => x.Id).IsValidGuid("Antibiotic ID");
    }
}