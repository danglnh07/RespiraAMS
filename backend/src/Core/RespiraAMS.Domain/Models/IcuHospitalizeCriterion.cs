using BuildingBlocks.Models;

namespace RespiraAMS.Domain.Models;

/// <summary>
/// ICU hospitalizing criteria
/// </summary>
public class IcuHospitalizeCriterion : Base
{
    public Guid DiseaseId { get; init; }
    public Disease Disease { get; set; } = null!;
    public Guid CriterionId { get; init; }
    public Criterion Criterion { get; set; } = null!;
    public bool IsMainCriteria { get; set; }
}