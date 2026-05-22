using System;
using System.ComponentModel;

namespace RespiraAMS.Application.Dtos;

public class ResistanceRiskFactorDtoRequest
{
    [Description("Disease ID")]
    public Guid DiseaseId  { get; set; }
    [Description("Pathogen ID")]
    public Guid PathogenId  { get; set; }
    [Description("Resistance risk factor criterion")]
    public CriterionDtoRequest Criterion { get; set; } = null!;
    [Description("Resistance risk factor name")]
    public string Name { get; set; } = string.Empty;
}

public class ResistanceRiskFactorDtoResponse
{
    [Description("Resistance risk factor ID")]
    public Guid Id { get; set; }
    [Description("Resistance risk factor Disease ID")]
    public Guid DiseaseId { get; set; }
    [Description("Resistance risk factor Pathogen")]
    public PathogenDtoResponse Pathogen { get; set; } = null!;
    [Description("Resistance risk factor criterion")]
    public CriterionDtoResponse Criterion { get; set; } = null!;
    [Description("Resistance risk factor name")]
    public string Name { get; set; } = string.Empty;
}