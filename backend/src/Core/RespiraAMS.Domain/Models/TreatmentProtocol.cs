using BuildingBlocks.Models;
using RespiraAMS.Domain.Enums;

namespace RespiraAMS.Domain.Models;

/// <summary>
/// Treatment protocol
/// </summary>
public class TreatmentProtocol : Base
{
    // Treatment protocol name
    public string Name { get; set; } = string.Empty;
    // Issuer (WHO, VietNam Ministry of Health,...)
    public string Issuer {get; set; } = string.Empty;
    // Date issuing
    public DateTimeOffset IssueDate { get; set; } = DateTimeOffset.UtcNow;
    // Treatment protocol version
    public int Version { get; set; }
    
    // Treatment protocol general information
    public Guid DiseaseId { get; set; }
    public Disease Disease { get; set; } = null!;
    public Severity Severity { get; set; }
    public TreatmentSite TreatmentSite { get; set; }

    // Risk of having special infection
    public Guid? SpecialInfectionId { get; set; }
    public Pathogen? SpecialInfection { get; set; }

    // Other sub-criteria
    public List<Guid> OtherCriteriaIds { get; set; } = [];
    public List<Criterion> OtherCriteria { get; set; } = [];

    // Medicines
    public List<Guid> MedicineIds { get; set; } = [];
    public List<Antibiotic> Medicines { get; set; } = [];
}