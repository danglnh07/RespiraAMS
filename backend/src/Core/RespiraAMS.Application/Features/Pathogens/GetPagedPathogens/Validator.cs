using FluentValidation;
using RespiraAMS.Application.Shared.Validations;

namespace RespiraAMS.Application.Features.Pathogens.GetPagedPathogens;

public class GetPagedPathogensValidator : AbstractValidator<GetPagedPathogensQuery>
{
    public GetPagedPathogensValidator()
    {
        RuleFor(x => x.Param).IsValidPaginationParam();
    }
}