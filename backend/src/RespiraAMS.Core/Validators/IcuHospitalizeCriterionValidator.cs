using FluentValidation;
using RespiraAMS.Core.Dtos;

namespace RespiraAMS.Core.Validators;

public class IcuHospitalizeCriterionValidator : AbstractValidator<IcuHospitalizedCriterionDtoRequest>
{
    public IcuHospitalizeCriterionValidator()
    {
        RuleFor(x => x.DiseaseId)
            .NotEqual(Guid.Empty)
            .WithMessage("Disease ID is required");
        RuleFor(x => x.Criterion)
            .SetValidator(new CriterionValidator());
    }
}