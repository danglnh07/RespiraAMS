using Microsoft.Extensions.DependencyInjection;
using RespiraAMS.Core.RepositoryContract;
using RespiraAMS.Core.ServiceContract;
using RespiraAMS.Core.Services;
using RespiraAMS.Infrastructure.Profiles;
using RespiraAMS.Infrastructure.Repository;
using RespiraAMS.Infrastructure.Services;

namespace RespiraAMS.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<CriterionProfile>();
        
        services.AddScoped<PathogenProfile>();
        services.AddScoped<IPathogenService, PathogenService>();

        services.AddScoped<AntibioticSpectrumProfile>();
        services.AddScoped<IAntibioticSpectrumService, AntibioticSpectrumService>();
        
        services.AddScoped<AntibioticProfile>();
        services.AddScoped<IAntibioticService, AntibioticService>();
        
        services.AddScoped<IcuHospitalizeCriterionProfile>();
        services.AddScoped<IIcuHospitalizeCriterionService, IcuHospitalizeCriterionService>();
        
        services.AddScoped<DiseaseProfile>();
        services.AddScoped<IDiseaseService, DiseaseService>();

        services.AddScoped<ResistanceRiskFactorProfile>();
        services.AddScoped<IResistanceRiskFactorService, ResistanceRiskFactorService>();

        services.AddScoped<DiseasePathogenProfile>();
        services.AddScoped<IDiseasePathogenService, DiseasePathogenService>();

        services.AddScoped<TreatmentProtocolProfile>();
        services.AddScoped<ITreatmentProtocolService, TreatmentProtocolService>();
    }
}