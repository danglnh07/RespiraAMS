using FluentValidation;

namespace RespiraAMS.Application.Features.AntibioticSpectra.GetPagedAntibioticSpectrum;

public class GetPagedAntibioticSpectrumValidator : AbstractValidator<GetPagedAntibioticSpectrumQuery>
{
    public GetPagedAntibioticSpectrumValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThan(0)
            .WithMessage("Page must be greater than zero");
        RuleFor(x => x.Size)
            .GreaterThan(0)
            .WithMessage("Size must be greater than zero");
    }
}