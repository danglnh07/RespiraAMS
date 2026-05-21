using System.ComponentModel;

namespace RespiraAMS.Application.Dtos;

public class IcuHospitalizedCriterionDtoRequest
{
    [Description("ICU hospitalize criterion")]
    public CriterionDtoRequest Criterion { get; set; } = null!;
    [Description("Disease ID")]
    public Guid DiseaseId { get; set; }
    [Description("Decide whether this criterion is a main or secondary criterion")]
    public bool IsMainCriteria { get; set; }
}

public class IcuHospitalizedCriterionDtoResponse
{
    [Description("ICU hospitalize criterion ID")]
    public Guid Id { get; set; }
    [Description("Disease ID")]
    public Guid DiseaseId { get; set; }
    [Description("ICU hospitalize criterion")]
    public CriterionDtoResponse Criterion { get; set; } = null!;
    [Description("Decide whether this criterion is a main or secondary criterion")]
    public bool IsMainCriteria { get; set; }
}