using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RespiraAMS.Application.Dtos;

namespace RespiraAMS.Application.Services.Contracts;

public interface IGenericService<in TRequest, TResponse>
{
    Task<Guid> CreateAsync(TRequest req);
    Task<TResponse> GetByIdAsync(Guid id);
    Task<(PaginationMetadata, IEnumerable<TResponse>)> GetListAsync(int page, int size);
    Task<TResponse> UpdateAsync(Guid id, TRequest req);
    Task DeleteAsync(Guid id);
}