using FluentValidation;
using RespiraAMS.Application.Shared;

namespace RespiraAMS.Application.Features.Pathogens.CreatePathogen;

public class CreatePathogenValidator : AbstractValidator<CreatePathogenCommand>
{
    public CreatePathogenValidator()
    {
        RuleFor(x => x.Name).NotEmptyString("Pathogen name");
        RuleFor(x => x.Description).NotEmptyString("Pathogen description");
    }
}