using FluentValidation;
using RespiraAMS.Application.Shared.Validations;

namespace RespiraAMS.Application.Features.Pathogens.DeletePathogen;

public class DeletePathogenValidator : AbstractValidator<DeletePathogenCommand>
{
    public DeletePathogenValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Pathogen ID is required");
    }
}