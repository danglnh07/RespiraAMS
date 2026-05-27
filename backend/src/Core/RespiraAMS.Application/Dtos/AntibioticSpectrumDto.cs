using System;
using System.ComponentModel;

namespace RespiraAMS.Application.Dtos;

public class AntibioticSpectrumDtoRequest
{
    [Description("Antibiotic spectrum name")]
    public string Name { get; set; } = string.Empty;  
    public string Description { get; set; } = string.Empty;
}

public class AntibioticSpectrumDtoResponse
{
    [Description("Antibiotic spectrum ID")]
    public Guid Id { get; set; }
    [Description("Antibiotic spectrum name")]
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}