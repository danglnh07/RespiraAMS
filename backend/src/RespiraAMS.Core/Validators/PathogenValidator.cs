using FluentValidation;
using RespiraAMS.Core.Dtos;

namespace RespiraAMS.Core.Validators;

public class PathogenValidator : AbstractValidator<PathogenDtoRequest>
{
    public PathogenValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Pathogen name is required");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Pathogen description is required");
    }
}