using RespiraAMS.Domain.Enums;

namespace RespiraAMS.Domain.Models;

/// <summary>
/// Tiêu chí đánh giá, một class abstract sẽ được dùng để biểu hiện tiêu chí đánh giá
/// </summary>
public abstract class Criterion : Base
{
    public string Name { get; set; } = string.Empty;
    public abstract CriterionType Type { get; }
}

/// <summary>
/// Tiêu chí đánh giá dạng True/False. Ở đây, mặc định True sẽ là thõa mãn tiêu chí
/// </summary>
public class BooleanCriterion : Criterion
{
    public override CriterionType Type => CriterionType.Boolean;
}

/// <summary>
/// Tiêu chí đánh giá dạng chỉ số (metrics). Class này hỗ trợ thiết lập linh hoạt các điều kiện chặn:
/// (min, max): không tính biên.
/// [min, max]: tính biên.
/// </summary>
public class NumericCriterion : Criterion
{
    public override CriterionType Type => CriterionType.Numeric;
    
    public double Min { get; set; }
    public double Max { get; set; }
    public string Unit { get; set; } = string.Empty;
    public bool IsExclusive { get; set; }
} 