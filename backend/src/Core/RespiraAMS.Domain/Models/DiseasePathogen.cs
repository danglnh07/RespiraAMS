using BuildingBlocks.Models;
using RespiraAMS.Domain.Enums;

namespace RespiraAMS.Domain.Models;

/// <summary>
/// Cause to a disease, which was categorized by severity and treatment site
/// </summary>
public class DiseasePathogen : Base
{
    public Guid DiseaseId { get; set; }
    public Disease Disease { get; set; } = null!;
    public Guid PathogenId { get; set; }
    public Pathogen Pathogen { get; set; } = null!;
    public Severity Severity { get; set; }
    public TreatmentSite TreatmentSite { get; set; }
}