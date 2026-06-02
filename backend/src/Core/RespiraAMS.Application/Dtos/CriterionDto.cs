using System;
using System.ComponentModel;
using RespiraAMS.Domain.Enums;

namespace RespiraAMS.Application.Dtos;

public class CriterionDtoRequest
{
    [Description("Criterion name")]
    public string Name { get; set; } = string.Empty;
    [Description("Criterion type. The value must be a string (case insensitive) value")]
    public CriterionType Type { get; set; }
    [Description("Criterion min value if this is numeric. No numeric constraints on min")]
    public double? Min { get; set; }
    [Description("Criterion max value if this is numeric. No numeric constraints on max")]
    public double? Max { get; set; }
    [Description("Criterion unit value if this is numeric. No constraints on unit")]
    public string? Unit { get; set; } = string.Empty;
    [Description("If true, then the min-max range doesn't consider the boundary value")]
    public bool? IsExclusive { get; set; } = false;
}

public class CriterionDtoResponse
{
    [Description("Criterion ID")]
    public Guid Id { get; set; }
    [Description("Criterion name")]
    public string Name { get; set; } = string.Empty;
    [Description("Criterion type")]
    public CriterionType Type { get; set; }
    [Description("Criterion min value if this is numeric. No numeric constraints on min")]
    public double? Min { get; set; }
    [Description("Criterion max value if this is numeric. No numeric constraints on max")]
    public double? Max { get; set; }
    [Description("Criterion unit value if this is numeric. No constraints on unit")]
    public string? Unit { get; set; } = string.Empty;
    [Description("If true, then the min-max range doesn't consider the boundary value")]
    public bool? IsExclusive { get; set; } = false;
}