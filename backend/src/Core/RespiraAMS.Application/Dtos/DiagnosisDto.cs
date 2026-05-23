using System;
using System.Collections.Generic;
using System.ComponentModel;
using RespiraAMS.Domain.Enums;

namespace RespiraAMS.Application.Dtos;

/*=== Diagnosis template DTO ===*/
public class DiagnosisTemplateDto
{
    public List<CriterionDtoResponse> IcuHospitalizeCriteria { get; set; } = [];
    public List<CriterionDtoResponse> ResistanceRiskFactors { get; set; } = [];
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
    [Description("ICU hospitalize criteria that patient has")]
    public List<Guid> IcuHospitalizeCriteria { get; set; } = [];
    [Description("Special antibiotic resistance pathogen risk factor that patient has")]
    public List<Guid> ResistanceRiskFactors { get; set; } = [];
    [Description("Other criteria that patient has")]
    public List<Guid> OtherCriteria { get; set; } = [];
}

public class InfectionProbabilityDto
{
    public PathogenDtoResponse Pathogen { get; set; } = null!;
    public double Probability { get; set; }
}

public class DiagnosisResultDto
{
    [Description("Patient severity")]
    public Severity Severity { get; set; }
    [Description("Patient treatment site")]
    public TreatmentSite TreatmentSite { get; set; }
    [Description("Probability of having special infection with pathogen that can resist antibiotic")]
    public List<InfectionProbabilityDto> InfectionProbabilities { get; set; } = [];
}

public class RecommendDtoRequest
{
    [Description("Patient severity")]
    public Severity Severity { get; set; }
    [Description("Patient treatment site")]
    public TreatmentSite TreatmentSite { get; set; }
    [Description("Probability of having special infection with pathogen that can resist antibiotic")]
    public Dictionary<Guid, double> InfectionProbabilities { get; set; } = [];
    [Description("Other criteria IDs that patient had")]
    public List<Guid> OtherCriteria { get; set; } = [];
}