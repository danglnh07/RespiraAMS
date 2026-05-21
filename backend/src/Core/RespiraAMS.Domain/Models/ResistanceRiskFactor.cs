namespace RespiraAMS.Domain.Models;

/*
 * Trường hợp của class này cũng tương tự IcuHospitalizeCriterion
 */

/// <summary>
/// Nguy cơ nhiễm khuẩn đặc biệt có khả năng kháng thuốc
/// </summary>
public class ResistanceRiskFactor : Base
{
    public Guid DiseaseId { get; init; }
    public Disease Disease { get; set; } = null!;
    public string Name { get; set; } = string.Empty;
    public Guid CriterionId { get; set; }
    public Criterion Criterion { get; set; } = null!;
    public Guid PathogenId { get; set; }
    public Pathogen Pathogen { get; set; } = null!;
}