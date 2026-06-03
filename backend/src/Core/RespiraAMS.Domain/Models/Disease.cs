using BuildingBlocks.Models;

namespace RespiraAMS.Domain.Models;

/// <summary>
/// Disease class. Treatment protocols won't be include in Disease because of how complex and nested it is
/// </summary>
public class Disease : Base
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public int RequiredIcuMainCriteria { get; set; }
    public int RequiredIcuSecondaryCriteria { get; set; }
    public List<IcuHospitalizeCriterion> IcuHospitalizeCriteria { get; init; } = [];
    public List<ResistanceRiskFactor> ResistanceRisks { get; init; } = [];
    public List<DiseasePathogen> DiseasePathogens {get; init;} = [];
    public List<TreatmentProtocol> TreatmentProtocols { get; init; } = [];
}