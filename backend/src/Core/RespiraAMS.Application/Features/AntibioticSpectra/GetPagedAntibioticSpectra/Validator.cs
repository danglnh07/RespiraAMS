using FluentValidation;
using RespiraAMS.Application.Shared.Validations;

namespace RespiraAMS.Application.Features.AntibioticSpectra.GetPagedAntibioticSpectra;

public class GetPagedAntibioticSpectrumValidator : AbstractValidator<GetPagedAntibioticSpectraQuery>
{
    public GetPagedAntibioticSpectrumValidator()
    {
        RuleFor(query => query.Param).IsValidPaginationParam();
    }
}