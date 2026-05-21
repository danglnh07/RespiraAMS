using System.ComponentModel;

namespace RespiraAMS.Application.Dtos;

public class PathogenDtoRequest
{
    [Description("Pathogen name")]
    public string Name { get; set; } = string.Empty;
    [Description("Pathogen description")]
    public string Description { get; set; } = string.Empty;
}

public class PathogenDtoResponse
{
    [Description("Pathogen ID")]
    public Guid Id { get; set; }
    [Description("Pathogen name")]
    public string Name { get; set; } = string.Empty;
    [Description("Pathogen description")]
    public string Description { get; set; } = string.Empty;
}