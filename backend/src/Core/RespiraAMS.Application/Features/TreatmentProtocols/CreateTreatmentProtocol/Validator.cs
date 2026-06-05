using FluentValidation;
using RespiraAMS.Application.Shared.Validations;

namespace RespiraAMS.Application.Features.TreatmentProtocols.CreateTreatmentProtocol;

public class CreateTreatmentProtocolValidator : AbstractValidator<CreateTreatmentProtocolCommand>
{
    public CreateTreatmentProtocolValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Treatment protocol name is required");
        RuleFor(x => x.Issuer)
            .NotEmpty()
            .WithMessage("Treatment protocol issuer is required");
        RuleFor(x => x.IssueDate)
            .Must(x => DateTimeOffset.Compare(DateTimeOffset.UtcNow, x) >= 0)
            .WithMessage("Treatment protocol issue date must not be in future");
        RuleFor(x => x.Severity)
            .IsInEnum()
            .WithMessage("Invalid value for treatment protocol severity");
        RuleFor(x => x.TreatmentSite)
            .IsInEnum()
            .WithMessage("Invalid value for treatment protocol treatment site");
        RuleFor(x => x.SpecialInfectionId)
            .Must(x => x is null || x != Guid.Empty)
            .WithMessage("Treatment protocol special infection id must not be empty (zero) UUID");
        RuleFor(x => x.OtherCriteriaIds)
            .NotEmpty()
            .WithMessage("Other criteria IDs must not be empty");
        RuleForEach(x => x.OtherCriteriaIds)
            .NotEmpty()
            .WithMessage("Other criteria ID must be a valid UUID");
        RuleFor(x => x.MedicineIds)
            .NotEmpty()
            .WithMessage("Medicine IDs must not be empty");
        RuleForEach(x => x.MedicineIds)
            .NotEmpty()
            .WithMessage("Medicine ID must be a valid UUID");
    }
}