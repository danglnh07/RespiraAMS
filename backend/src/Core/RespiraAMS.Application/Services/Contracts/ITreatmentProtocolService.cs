using RespiraAMS.Application.Dtos;

namespace RespiraAMS.Application.Services.Contracts;

public interface ITreatmentProtocolService : IGenericService<TreatmentProtocolDtoRequest, TreatmentProtocolDtoResponse>
{
    // Add new criteria (that not belong to IcuHospitalizeCriteria and ResistanceRiskFactor)
    Task AddCriteriaToProtocol(Guid id, List<CriterionDtoRequest> req);
}
