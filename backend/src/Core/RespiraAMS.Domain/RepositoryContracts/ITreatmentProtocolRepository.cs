using RespiraAMS.Domain.Models;

namespace RespiraAMS.Domain.RepositoryContracts;

public interface ITreatmentProtocolRepository : IGenericRepository<TreatmentProtocol>
{
    Task<TreatmentProtocol?> GetTreatmentProtocolByIdAsNoTrackingAsync(Guid id, Func<IQueryable<TreatmentProtocol>, IQueryable<TreatmentProtocol>>? query);
}