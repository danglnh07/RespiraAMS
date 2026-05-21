namespace RespiraAMS.Core.Models;

/// <summary>
/// Bệnh. Sẽ bao gồm hết các thông tin liên quan đến bệnh như các tiêu chí tính toán
/// mức độ hay các tình trạng đặc biệt, tác nhân gây bệnh,... Phác đồ điều trị sẽ không
/// nằm trong này
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