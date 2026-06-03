using FluentValidation;
using RespiraAMS.Application.Shared;

namespace RespiraAMS.Application.Features.AntibioticSpectra.GetPagedAntibioticSpectrum;

public class GetPagedAntibioticSpectrumValidator : AbstractValidator<GetPagedAntibioticSpectrumQuery>
{
    public GetPagedAntibioticSpectrumValidator()
    {
        RuleFor(query => query.Param).IsValidPaginationParam();
    }
}