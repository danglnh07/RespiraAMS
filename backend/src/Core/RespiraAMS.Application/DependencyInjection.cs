using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using RespiraAMS.Application.Abstracts.Mappers;
using RespiraAMS.Application.Features.Antibiotics.CreateAntibiotic;
using RespiraAMS.Application.Features.Antibiotics.UpdateAntibiotic;
using RespiraAMS.Application.Features.AntibioticSpectra.CreateAntibioticSpectrum;
using RespiraAMS.Application.Features.AntibioticSpectra.UpdateAntibioticSpectrum;
using RespiraAMS.Application.Features.DiseasePathogens.CreateDiseasePathogen;
using RespiraAMS.Application.Features.DiseasePathogens.UpdateDiseasePathogen;
using RespiraAMS.Application.Features.Diseases.CreateDisease;
using RespiraAMS.Application.Features.Diseases.UpdateDisease;
using RespiraAMS.Application.Features.IcuHospitalizeCriteria.CreateIcuHospitalizeCriterion;
using RespiraAMS.Application.Features.IcuHospitalizeCriteria.UpdateIcuHospitalizeCriterion;
using RespiraAMS.Application.Features.Pathogens.CreatePathogen;
using RespiraAMS.Application.Features.Pathogens.UpdatePathogen;
using RespiraAMS.Application.Features.ResistanceRiskFactors.CreateResistanceRiskFactor;
using RespiraAMS.Application.Features.ResistanceRiskFactors.UpdateResistanceRiskFactor;
using RespiraAMS.Application.Features.TreatmentProtocols.CreateTreatmentProtocol;
using RespiraAMS.Application.Features.TreatmentProtocols.UpdateTreatmentProtocol;
using RespiraAMS.Application.Shared.Dtos;
using RespiraAMS.Application.Shared.Mappers;
using RespiraAMS.Domain.Models;

namespace RespiraAMS.Application;

/// <summary>
///  This is just a dump class used for scanning assembly, it has no meaning in application code
/// </summary>
public class ApplicationMarker;

public static class DependencyInjection
{
    public static void AddProfiles(this IServiceCollection services)
    {
        services.AddScoped<ICreateMapper<Criterion, CreateCriterionCommand>, CreateCriterionMapper>();
        services.AddScoped<IUpdateMapper<Criterion, UpdateCriterionCommand>, UpdateCriterionMapper>();
        services.AddScoped<IResultMapper<Criterion, CriterionItem>, CriterionResultMapper>();
        
        services.AddScoped<ICreateMapper<AntibioticSpectrum, CreateAntibioticSpectrumCommand>,
            CreateAntibioticSpectrumMapper>();
        services.AddScoped<IUpdateMapper<AntibioticSpectrum, UpdateAntibioticSpectrumCommand>,
            UpdateAntibioticSpectrumMapper>();

        services.AddScoped<ICreateMapper<Antibiotic, CreateAntibioticCommand>, CreateAntibioticMapper>();
        services.AddScoped<IUpdateMapper<Antibiotic, UpdateAntibioticCommand>, UpdateAntibioticMapper>();

        services.AddScoped<ICreateMapper<DiseasePathogen, CreateDiseasePathogenCommand>, CreateDiseasePathogenMapper>();
        services.AddScoped<IUpdateMapper<DiseasePathogen, UpdateDiseasePathogenCommand>, UpdateDiseasePathogenMapper>();

        services.AddScoped<ICreateMapper<Disease, CreateDiseaseCommand>, CreateDiseaseMapper>();
        services.AddScoped<IUpdateMapper<Disease, UpdateDiseaseCommand>, UpdateDiseaseMapper>();

        services.AddScoped<ICreateMapper<IcuHospitalizeCriterion, CreateIcuHospitalizeCriterionCommand>,
            CreateIcuHospitalizeCriterionMapper>();
        services.AddScoped<IUpdateMapper<IcuHospitalizeCriterion, UpdateIcuHospitalizeCriterionCommand>,
            UpdateIcuHospitalizeCriterionMapper>();

        services.AddScoped<ICreateMapper<Pathogen, CreatePathogenCommand>, CreatePathogenMapper>();
        services.AddScoped<IUpdateMapper<Pathogen, UpdatePathogenCommand>, UpdatePathogenMapper>();

        services.AddScoped<ICreateMapper<ResistanceRiskFactor, CreateResistanceRiskFactorCommand>,
            CreateResistanceRiskFactorMapper>();
        services.AddScoped<IUpdateMapper<ResistanceRiskFactor, UpdateResistanceRiskFactorCommand>,
            UpdateResistanceRiskFactorMapper>();

        services.AddScoped<ICreateMapper<TreatmentProtocol, CreateTreatmentProtocolCommand>,
            CreateTreatmentProtocolMapper>();
        services.AddScoped<IUpdateMapper<TreatmentProtocol, UpdateTreatmentProtocolCommand>,
            UpdateTreatmentProtocolMapper>();
    }

    public static void AddFluentValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<ApplicationMarker>();
    }
}