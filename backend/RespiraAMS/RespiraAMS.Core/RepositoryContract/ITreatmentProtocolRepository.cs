using RespiraAMS.Core.Models;

namespace RespiraAMS.Core.RepositoryContract;

public interface ITreatmentProtocolRepository : IGenericRepository<TreatmentProtocol>
{
    Task<TreatmentProtocol?> GetTreatmentProtocolByIdAsNoTrackingAsync(Guid id, Func<IQueryable<TreatmentProtocol>, IQueryable<TreatmentProtocol>>? query);
}