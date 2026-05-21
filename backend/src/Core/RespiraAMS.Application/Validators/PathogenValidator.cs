using FluentValidation;
using RespiraAMS.Application.Dtos;

namespace RespiraAMS.Application.Validators;

public class PathogenValidator : AbstractValidator<PathogenDtoRequest>
{
    public PathogenValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Pathogen name is required");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Pathogen description is required");
    }
}