using FluentValidation;
using RespiraAMS.Application.Dtos;

namespace RespiraAMS.Application.Validators;

public class IcuHospitalizeCriterionValidator : AbstractValidator<IcuHospitalizedCriterionDtoRequest>
{
    public IcuHospitalizeCriterionValidator()
    {
        RuleFor(x => x.DiseaseId)
            .NotEqual(Guid.Empty)
            .WithMessage("Disease ID is required");
        RuleFor(x => x.Criterion)
            .SetValidator(new Application.Validators.CriterionValidator());
    }
}