using System;
using System.Collections.Generic;
using System.ComponentModel;
using RespiraAMS.Domain.Enums;

namespace RespiraAMS.Application.Dtos;

/*=== Diagnosis template DTO ===*/
public class DiagnosisTemplateDto
{
    [Description("ICU hospitalize criteria belong to that disease")]
    public List<CriterionDtoResponse> IcuHospitalizeCriteria { get; set; } = [];
    [Description("Resistance risk factors belong to that disease")]
    public List<CriterionDtoResponse> ResistanceRiskFactors { get; set; } = [];
    [Description("Other sub criteria that disease can have")]
    public List<CriterionDtoResponse> OtherCriteria { get; set; } = [];
} 

public class ClinicalPictureDto
{
    [Description("Are there any symptoms of decreased consciousness?")]
    public bool Confusion { get; set; }
    [Description("Urea blood pressure (mmol/L)")]
    public double? Urea { get; set; }
    [Description("Respiratory rate per minute")]
    public int Respiratory { get; set; }
    [Description("Systolic blood pressure (mmHg)")]
    public int Systolic { get; set; }
    [Description("Diastolic blood pressure (mmHg)")]
    public int Diastolic { get; set; }
    [Description("Age")]
    public int Age { get; set; }
    [Description("ICU hospitalize criteria that patient has. It is the criteria ID, not the IcuHospitalizeCriteria ID")]
    public List<Guid> IcuHospitalizeCriteria { get; set; } = [];
    [Description("Special antibiotic resistance pathogen risk factor that patient has. It is the criteria ID, not the ResistanceRiskFactor ID")]
    public List<Guid> ResistanceRiskFactors { get; set; } = [];
    [Description("Other criteria that patient has")]
    public List<Guid> OtherCriteria { get; set; } = [];
}

public class InfectionProbabilityDto
{
    [Description("Pathogen")]
    public PathogenDtoResponse Pathogen { get; set; } = null!;
    [Description("Probability of having infection. Its value is in range [0, 1]")]
    public double Probability { get; set; }
}

public class DiagnosisResultDto
{
    [Description("Patient severity")]
    public Severity Severity { get; set; }
    [Description("Patient treatment site")]
    public TreatmentSite TreatmentSite { get; set; }
    [Description("Probabilities of having special infection with pathogen that can resist antibiotic")]
    public List<InfectionProbabilityDto> InfectionProbabilities { get; set; } = [];
}

public class RecommendDtoRequest
{
    [Description("Patient severity")]
    public Severity Severity { get; set; }
    [Description("Patient treatment site")]
    public TreatmentSite TreatmentSite { get; set; }
    [Description("Probabilities of having special infection. The key is the pathogen ID and value is it probability")]
    public Dictionary<Guid, double> InfectionProbabilities { get; set; } = [];
    [Description("Other criteria IDs that patient had")]
    public List<Guid> OtherCriteria { get; set; } = [];
}