using FluentValidation;
using RespiraAMS.Application.Shared.Validations;

namespace RespiraAMS.Application.Features.Diseases.GetPagedDiseases;

public class GetPagedDiseasesValidator : AbstractValidator<GetPagedDiseasesQuery>
{
    public GetPagedDiseasesValidator()
    {
        RuleFor(x => x.Param).IsValidPaginationParam();
    }
}