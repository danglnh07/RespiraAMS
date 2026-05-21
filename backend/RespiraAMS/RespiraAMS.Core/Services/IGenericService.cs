using RespiraAMS.Core.Dtos;
using RespiraAMS.Core.Models;

namespace RespiraAMS.Core.ServiceContract;

public interface IGenericService<in TRequest, TResponse>
{
    Task<Guid> CreateAsync(TRequest req);
    Task<TResponse> GetByIdAsync(Guid id);
    Task<(PaginationMetadata, IEnumerable<TResponse>)> GetListAsync(int page, int size);
    Task<TResponse> UpdateAsync(Guid id, TRequest req);
    Task DeleteAsync(Guid id);
}