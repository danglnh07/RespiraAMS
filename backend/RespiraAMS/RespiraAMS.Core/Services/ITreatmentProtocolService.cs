using RespiraAMS.Core.Dtos;
using RespiraAMS.Core.ServiceContract;

namespace RespiraAMS.Core.Services;

public interface ITreatmentProtocolService : IGenericService<TreatmentProtocolDtoRequest, TreatmentProtocolDtoResponse>
{
    // Add new criteria (that not belong to IcuHospitalizeCriteria and ResistanceRiskFactor)
    Task AddCriteriaToProtocol(Guid id, List<CriterionDtoRequest> req);
}
