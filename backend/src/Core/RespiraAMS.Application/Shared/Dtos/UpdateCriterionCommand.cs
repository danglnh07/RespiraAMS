using RespiraAMS.Domain.Enums;

namespace RespiraAMS.Application.Shared.Dtos;

public class UpdateCriterionCommand
{
    public string Name { get; set; } = string.Empty;
    public CriterionType Type { get; set; }
    public double? Min { get; set; }
    public double? Max { get; set; }
    public string? Unit { get; set; }
    public bool? IsExclusive { get; set; }
}