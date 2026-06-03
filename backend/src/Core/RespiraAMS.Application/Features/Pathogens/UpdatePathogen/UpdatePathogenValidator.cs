using FluentValidation;
using RespiraAMS.Application.Shared;

namespace RespiraAMS.Application.Features.Pathogens.UpdatePathogen;

public class UpdatePathogenValidator : AbstractValidator<UpdatePathogenCommand>
{
    public UpdatePathogenValidator()
    {
        RuleFor(x => x.Id).IsValidGuid("Pathogen ID");
        RuleFor(x => x.Name).NotEmptyString("Pathogen name");
        RuleFor(x => x.Description).NotEmptyString("Pathogen description");
    }
}