using BuildingBlocks.Models;

namespace RespiraAMS.Domain.Models;

/// <summary>
/// Pathogen (which can either be virus, bacterium,...). This pathogen won't associate with any disease
/// </summary>
public class Pathogen : Base
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<ResistanceRiskFactor> ResistanceRiskFactors { get; set; } = [];
    public List<DiseasePathogen> DiseasePathogens  { get; set; } = [];
    public List<TreatmentProtocol> TreatmentProtocols { get; set; } = [];
}