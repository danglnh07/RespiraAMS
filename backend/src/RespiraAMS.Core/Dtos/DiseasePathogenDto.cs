using System.ComponentModel;
using RespiraAMS.Core.Enums;

namespace RespiraAMS.Core.Dtos;

public class DiseasePathogenDtoRequest
{
    [Description("Disease ID")]
    public Guid DiseaseId {get; set;}
    [Description("Pathogen ID")]
    public Guid PathogenId { get; set; }
    [Description("Disease severity. Can either be number or case insensitive string")]
    public Severity Severity {get; set;}
    [Description("Treatment site. Can either be number or case insensitive string")]
    public TreatmentSite TreatmentSite {get; set;}
}

public class DiseasePathogenDtoResponse
{
    [Description("Disease pathogen ID")]
    public Guid Id {get; set;}
    [Description("Pathogen")]
    public PathogenDtoResponse Pathogen { get; set; } = null!;
    [Description("Disease Severity")]
    public Severity Severity {get; set;}
    [Description("Treatment site")]
    public TreatmentSite TreatmentSite { get; set; }
}