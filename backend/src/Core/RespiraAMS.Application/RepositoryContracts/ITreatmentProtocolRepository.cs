using System;
using System.Linq;
using System.Threading.Tasks;
using RespiraAMS.Domain.Models;

namespace RespiraAMS.Application.RepositoryContracts;

public interface ITreatmentProtocolRepository : IGenericRepository<TreatmentProtocol>
{
    Task<TreatmentProtocol?> GetTreatmentProtocolByIdAsNoTrackingAsync(Guid id, Func<IQueryable<TreatmentProtocol>, IQueryable<TreatmentProtocol>>? query);
}