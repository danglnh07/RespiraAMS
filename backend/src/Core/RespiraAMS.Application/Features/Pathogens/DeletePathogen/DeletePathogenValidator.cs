using FluentValidation;
using RespiraAMS.Application.Shared;

namespace RespiraAMS.Application.Features.Pathogens.DeletePathogen;

public class DeletePathogenValidator : AbstractValidator<DeletePathogenCommand>
{
    public DeletePathogenValidator()
    {
        RuleFor(command => command.Id).IsValidGuid("Pathogen ID");
    }
}