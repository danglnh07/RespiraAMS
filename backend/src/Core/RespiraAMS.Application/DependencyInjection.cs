using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using RespiraAMS.Application.Features.Antibiotics.CreateAntibiotic;
using RespiraAMS.Application.Features.Antibiotics.DeleteAntibiotic;
using RespiraAMS.Application.Features.Antibiotics.GetPagedAntibiotic;
using RespiraAMS.Application.Features.Antibiotics.UpdateAntibiotic;
using RespiraAMS.Application.Features.AntibioticSpectra.CreateAntibioticSpectrum;
using RespiraAMS.Application.Features.AntibioticSpectra.DeleteAntibioticSpectrum;
using RespiraAMS.Application.Features.AntibioticSpectra.GetPagedAntibioticSpectrum;
using RespiraAMS.Application.Features.AntibioticSpectra.UpdateAntibioticSpectrum;
using RespiraAMS.Application.Mappers;

namespace RespiraAMS.Application;

public static class DependencyInjection
{
    public static void AddProfiles(this IServiceCollection services)
    {
        services.AddScoped<AntibioticSpectrumMapper>();
    }

    public static void AddFluentValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(CreateAntibioticSpectrumValidator).Assembly);
        services.AddValidatorsFromAssembly(typeof(UpdateAntibioticSpectrumValidator).Assembly);
        services.AddValidatorsFromAssembly(typeof(GetPagedAntibioticSpectrumValidator).Assembly);
        services.AddValidatorsFromAssembly(typeof(DeleteAntibioticSpectrumValidator).Assembly);
        
        services.AddValidatorsFromAssembly(typeof(CreateAntibioticValidator).Assembly);
        services.AddValidatorsFromAssembly(typeof(UpdateAntibioticValidator).Assembly);
        services.AddValidatorsFromAssembly(typeof(GetPagedAntibioticValidator).Assembly);
        services.AddValidatorsFromAssembly(typeof(DeleteAntibioticValidator).Assembly);
    }
}