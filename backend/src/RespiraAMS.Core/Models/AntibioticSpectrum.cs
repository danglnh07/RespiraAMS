namespace RespiraAMS.Core.Models;

/// <summary>
/// Antibiotic spectrum
/// </summary>
public class AntibioticSpectrum : Base
{
    public string Name { get; set; } = string.Empty;
    public List<Antibiotic> Antibiotics { get; set; } = [];
}