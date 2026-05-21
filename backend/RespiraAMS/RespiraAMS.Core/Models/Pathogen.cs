namespace RespiraAMS.Core.Models;

/// <summary>
/// Tác nhân gây bệnh (chung). Có thể là vi khuẩn, virus, nấm,...
/// </summary>
public class Pathogen : Base
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<ResistanceRiskFactor> ResistanceRiskFactors { get; set; } = [];
    public List<DiseasePathogen> DiseasePathogens  { get; set; } = [];
    public List<TreatmentProtocol> TreatmentProtocols { get; set; } = [];
}