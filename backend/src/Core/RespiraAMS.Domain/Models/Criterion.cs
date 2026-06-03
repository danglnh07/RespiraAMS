using BuildingBlocks.Models;
using RespiraAMS.Domain.Enums;

namespace RespiraAMS.Domain.Models;

/// <summary>
/// Criterion used to evaluate condition, severity,...
/// </summary>
public abstract class Criterion : Base
{
    public string Name { get; set; } = string.Empty;
    public abstract CriterionType Type { get; }
}

/// <summary>
/// This criterion is a True/False type
/// </summary>
public class BooleanCriterion : Criterion
{
    public override CriterionType Type => CriterionType.Boolean;
}

/// <summary>
/// Metric-type criterion.
/// </summary>
public class NumericCriterion : Criterion
{
    public override CriterionType Type => CriterionType.Numeric;
    
    public double Min { get; set; }
    public double Max { get; set; }
    public string Unit { get; set; } = string.Empty;
    public bool IsExclusive { get; set; }
} 