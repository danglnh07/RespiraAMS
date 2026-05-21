namespace RespiraAMS.Core.Models;

/*
 * Mỗi tiêu chí (Criterion) sẽ tương ứng với một tiêu chí nhập ICU (1-1 relationship)
 * thay vì (1-many) -> Nếu nhiều bệnh xài chung 1 tiêu chí thì bảng Criterion sẽ bị lặp dữ liệu:
 * Pros: mỗi criterion gắn liền với IcuCriterion -> Việc update sẽ rất đơn giản, thay đổi tiêu chí ở bệnh
 * này không ảnh hưởng đến bệnh khác, thay vì 1-many thì phải tạo mới 1 tiêu chí
 * Cons: trùng lặp dữ liệu trên bảng Criterion
 */

/// <summary>
/// Tiêu chí nhập ICU.
/// </summary>
public class IcuHospitalizeCriterion : Base
{
    public Guid DiseaseId { get; init; }
    public Disease Disease { get; set; } = null!;
    public Guid CriterionId { get; init; }
    public Criterion Criterion { get; set; } = null!;
    public bool IsMainCriteria { get; set; }
}