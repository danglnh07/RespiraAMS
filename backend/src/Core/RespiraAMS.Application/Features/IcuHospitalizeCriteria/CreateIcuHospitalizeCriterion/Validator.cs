using FluentValidation;
using RespiraAMS.Application.Shared.Validations;

namespace RespiraAMS.Application.Features.IcuHospitalizeCriteria.CreateIcuHospitalizeCriterion;

public class CreateIcuHospitalizeCriterionValidation : AbstractValidator<CreateIcuHospitalizeCriterionCommand>
{
    public CreateIcuHospitalizeCriterionValidation()
    {
        RuleFor(x => x.DiseaseId)
            .NotEmpty()
            .WithMessage("Disease ID is required");
        RuleFor(x => x.Criterion)
            .SetValidator(new CreateCriterionValidator());
    }
}
