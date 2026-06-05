using System.Text.Json.Serialization;
using RespiraAMS.Domain.Enums;

namespace RespiraAMS.Application.Shared.Dtos;

public class CriterionItem
{
    public string Name { get; set; } = string.Empty;
    public CriterionType Type { get; set; }
    public Guid Id { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public double? Min { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public double? Max { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Unit { get; set; } = string.Empty;
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? IsExclusive { get; set; } = false;
}