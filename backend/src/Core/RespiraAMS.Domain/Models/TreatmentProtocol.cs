using RespiraAMS.Domain.Enums;

namespace RespiraAMS.Domain.Models;

/*
 * Phác đồ điêu trị có thể được phân loại dựa trên các yếu tố sau:
 * 1. Mức độ (Severity) và nơi (cấp độ) điều trị (TreatmentSite)
 * 2. Tỉ lệ nhiễm khuẩn đặc biệt
 * 3. Các tiêu chí phụ khác
 * Trong đó, 1. là bắt buộc, 2. và 3. là tùy chọn
 */

/// <summary>
/// Phác đồ điều trị.
/// </summary>
public class TreatmentProtocol : Base
{
    public Guid DiseaseId { get; set; }
    public Disease Disease { get; set; } = null!;
    public int Version { get; set; }
    public Severity Severity { get; set; }
    public TreatmentSite TreatmentSite { get; set; }

    // Nhiễm khuẩn đặc biệt
    public Guid? SpecialInfectionId { get; set; }
    public Pathogen? SpecialInfection { get; set; }

    // Các tiêu chí phụ khác
    public List<Guid> OtherCriteriaIds { get; set; } = [];
    public List<Criterion> OtherCriteria { get; set; } = [];

    // Thuốc
    public List<Guid> MedicineIds { get; set; } = [];
    public List<Antibiotic> Medicines { get; set; } = [];
}