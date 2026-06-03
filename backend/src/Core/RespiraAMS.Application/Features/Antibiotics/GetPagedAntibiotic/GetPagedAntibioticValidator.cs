using FluentValidation;
using RespiraAMS.Application.Shared;

namespace RespiraAMS.Application.Features.Antibiotics.GetPagedAntibiotic;

public class GetPagedAntibioticValidator : AbstractValidator<GetPagedAntibioticQuery>
{
    public GetPagedAntibioticValidator()
    {
        RuleFor(x => x.Param).IsValidPaginationParam();
        // Strictly enough, filter doesn't necessary need to be correct, so we won't validate any filter field 
    }
}