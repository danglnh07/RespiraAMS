using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RespiraAMS.Domain.Models;
using X.PagedList;

namespace RespiraAMS.Application.RepositoryContracts;

public interface IGenericRepository<T> where T : Base
{
    Task CreateAsync(T entity);
    Task<T?> GetByIdAsync(Guid id);
    Task<T?> GetByIdAsync(Guid id, Func<IQueryable<T>, IQueryable<T>>? query);
    Task<IPagedList<T>> GetPagedListAsync(int page, int size);
    Task<IPagedList<T>> GetPagedListAsync(int page, int size, Func<IQueryable<T>, IQueryable<T>>? query);
    void Update(T entity, bool isEntityTracked = true);
    void SoftDelete(T entity);
    Task<bool> AllIdsExistAsync(List<Guid> ids);
    T AttachStub(Guid id);
    void UpdateRelations(ICollection<T> collection, IEnumerable<Guid>? ids);
}