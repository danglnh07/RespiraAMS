using System;
using System.Collections.Generic;
using System.ComponentModel;
using RespiraAMS.Domain.Enums;

namespace RespiraAMS.Application.Dtos;

public class TreatmentProtocolDtoRequest
{
    [Description("Disease ID")]
    public Guid DiseaseId { get; set; }
    [Description("Treatment protocol version. Must be greater than 0")]
    public int Version { get; set; }
    [Description("Severity. Can be either number or case insensitive string")]
    public Severity Severity { get; set; }
    [Description("Treatment site. Can be either number or case insensitive string")]
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
    public PathogenDtoResponse? SpecialInfection { get; set; } = null!;
    [Description("Other secondary criteria")]
    public List<CriterionDtoResponse> OtherCriteria { get; set; } = [];
    [Description("Treatment protocol medicines")]
    public List<AntibioticDtoResponse> Medicines { get; set; } = [];
}