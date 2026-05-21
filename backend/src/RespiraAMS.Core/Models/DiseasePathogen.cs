using RespiraAMS.Core.Enums;

namespace RespiraAMS.Core.Models;

/// <summary>
/// Disease causes
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