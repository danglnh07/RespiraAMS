using System.ComponentModel;

namespace RespiraAMS.Application.Dtos;

public class DiseaseDtoRequest
{
    [Description("Disease name")]
    public string Name { get; set; } = string.Empty;
    [Description("Disease description")]
    public string Description { get; set; } = string.Empty;
    [Description("Required main criteria to get ICU hospitalized. Must be greater than 0")]
    public int RequiredIcuMainCriteria { get; set; } = 0;
    [Description("Required secondary criteria to get ICU hospitalized. Must be greater than 0")]
    public int RequiredIcuSecondaryCriteria { get; set; } = 0;
}

public class DiseaseDtoResponse
{
    [Description("Disease ID")]
    public Guid Id { get; set; }
    [Description("Disease name")]
    public string Name { get; set; } = string.Empty;
    [Description("Disease description")]
    public string Description { get; set; } = string.Empty;
    [Description("Required main criteria to get ICU hospitalized. Must be greater than 0")]
    public int RequiredIcuMainCriteria { get; set; } = 0;
    [Description("Required secondary criteria to get ICU hospitalized. Must be greater than 0")]
    public int RequiredIcuSecondaryCriteria { get; set; } = 0;
    public List<IcuHospitalizedCriterionDtoResponse> IcuHospitalizedCriteria { get; set; } = [];
    public List<ResistanceRiskFactorDtoResponse> ResistanceRisks { get; set; } = [];
    public List<DiseasePathogenDtoResponse> DiseasePathogens { get; set; } = [];
}