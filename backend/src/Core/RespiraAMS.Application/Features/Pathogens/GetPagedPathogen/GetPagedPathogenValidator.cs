using FluentValidation;
using RespiraAMS.Application.Shared;

namespace RespiraAMS.Application.Features.Pathogens.GetPagedPathogen;

public class GetPagedPathogenValidator : AbstractValidator<GetPagedPathogenQuery>
{
    public GetPagedPathogenValidator()
    {
        RuleFor(x => x.Param).IsValidPaginationParam();
    }
}