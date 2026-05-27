using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using RespiraAMS.Application.Profiles.Implementations;
using RespiraAMS.Application.Services.Contracts;
using RespiraAMS.Application.Services.Implementations;
using RespiraAMS.Application.Validators;

namespace RespiraAMS.Application;

public static class DependencyInjection
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAntibioticService, AntibioticService>();
        services.AddScoped<IAntibioticSpectrumService, AntibioticSpectrumService>();
        services.AddScoped<IDiseasePathogenService, DiseasePathogenService>();
        services.AddScoped<IDiseaseService, DiseaseService>();
        services.AddScoped<IIcuHospitalizeCriterionService, IcuHospitalizeCriterionService>();
        services.AddScoped<IPathogenService, PathogenService>();
        services.AddScoped<IResistanceRiskFactorService, ResistanceRiskFactorService>();
        services.AddScoped<ITreatmentProtocolService, TreatmentProtocolService>();
        services.AddScoped<IDiagnoseService, DiagnoseService>();
    }

    public static void AddProfiles(this IServiceCollection services)
    {
        services.AddScoped<AntibioticProfile>();
        services.AddScoped<AntibioticSpectrumProfile>();
        services.AddScoped<CriterionProfile>();
        services.AddScoped<DiseasePathogenProfile>();
        services.AddScoped<DiseaseProfile>();
        services.AddScoped<IcuHospitalizeCriterionProfile>();
        services.AddScoped<PathogenProfile>();
        services.AddScoped<ResistanceRiskFactorProfile>();
        services.AddScoped<TreatmentProtocolProfile>();
    }

    public static void AddFluentValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(AntibioticSpectrumValidator).Assembly);
        services.AddValidatorsFromAssembly(typeof(AntibioticValidator).Assembly);
        services.AddValidatorsFromAssembly(typeof(CriterionValidator).Assembly);
        services.AddValidatorsFromAssembly(typeof(DiseasePathogenValidator).Assembly);
        services.AddValidatorsFromAssembly(typeof(DiseasePathogenValidator).Assembly);
        services.AddValidatorsFromAssembly(typeof(DiseaseValidator).Assembly);
        services.AddValidatorsFromAssembly(typeof(IcuHospitalizeCriterionProfile).Assembly);
        services.AddValidatorsFromAssembly(typeof(PathogenValidator).Assembly);
        services.AddValidatorsFromAssembly(typeof(ResistanceRiskFactorProfile).Assembly);
        services.AddValidatorsFromAssembly(typeof(TreatmentProtocolValidator).Assembly);
    }
}