using System;
using System.Collections.Generic;
using System.ComponentModel;
using RespiraAMS.Domain.Enums;

namespace RespiraAMS.Application.Dtos;

public class TreatmentProtocolDtoRequest
{
    [Description("Treatment protocol name")]
    public string Name { get; set; } = string.Empty;
    [Description("Disease ID")]
    public Guid DiseaseId { get; set; }
    [Description("Treatment protocol version. Must be greater than 0")]
    public int Version { get; set; }
    [Description("Severity. The value must be a string (case insensitive) value")]
    public Severity Severity { get; set; }
    [Description("Treatment site. The value must be a string (case insensitive) value")]
    public TreatmentSite TreatmentSite { get; set; }
    [Description("Special infection ID, which is the Pathogen ID")]
    public Guid? SpecialInfectionId { get; set; }
    [Description("Other secondary criteria ids. If given, it should be existed ID")]
    public List<Guid> OtherCriteriaIds { get; set; } = [];
    [Description("Medicine ids (Antibiotic ids)")]
    public List<Guid> MedicineIds { get; set; } = [];
}

public class TreatmentProtocolDtoResponse
{
    [Description("Treatment ptotocol ID")]
    public Guid Id { get; set; }
    [Description("Treatment protocol name")]
    public string Name { get; set; } = string.Empty;
    [Description("Updated time with UTC")]
    public DateTimeOffset UpdatedAt { get; set; }
    [Description("Disease ID")]
    public Guid DiseaseId { get; set; }
    [Description("Treatment protocol version")]
    public int Version { get; set; }
    [Description("Severity")]
    public Severity Severity { get; set; }
    [Description("Treatment site")]
    public TreatmentSite TreatmentSite { get; set; }
    [Description("Special infection pathogen")]
    public PathogenDtoResponse? SpecialInfection { get; set; }
    [Description("Other secondary criteria")]
    public List<CriterionDtoResponse> OtherCriteria { get; set; } = [];
    [Description("Treatment protocol medicines")]
    public List<AntibioticDtoResponse> Medicines { get; set; } = [];
}