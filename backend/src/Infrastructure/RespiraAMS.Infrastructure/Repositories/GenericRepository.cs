using Microsoft.EntityFrameworkCore;
using RespiraAMS.Domain.Models;
using RespiraAMS.Domain.RepositoryContracts;
using RespiraAMS.Infrastructure.Data;
using X.PagedList;
using X.PagedList.EF;

namespace RespiraAMS.Infrastructure.Repositories;

public class GenericRepository<T>(AppDbContext context) : IGenericRepository<T> where T : Base
{
    public async Task CreateAsync(T entity)
    {
        await context.Set<T>().AddAsync(entity);
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        var model = await context.Set<T>().FindAsync(id);
        return model;
    }

    public async Task<T?> GetByIdAsync(Guid id, Func<IQueryable<T>, IQueryable<T>>? query)
    {
        var queryable = context.Set<T>().AsQueryable();
        if (query is not null)
        {
            queryable = query(queryable);
        }

        return await queryable.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IPagedList<T>> GetPagedListAsync(int page, int size)
    {
        var models = await context.Set<T>()
            .OrderByDescending(x => x.CreatedAt)
            .ToPagedListAsync(page, size);
        return models;
    }

    public async Task<IPagedList<T>> GetPagedListAsync(int page, int size, Func<IQueryable<T>, IQueryable<T>>? query)
    {
        var queryable = context.Set<T>().AsQueryable();
        if (query is not null)
        {
            queryable = query(queryable);
        }

        return await queryable
            .OrderByDescending(x => x.CreatedAt)
            .ToPagedListAsync(page, size);
    }

    public void Update(T entity, bool isEntityTracked = true)
    {
        entity.UpdatedAt = DateTimeOffset.UtcNow;
        if (!isEntityTracked)
        {
            context.Set<T>().Update(entity);
        }
    }

    public void SoftDelete(T entity)
    {
        entity.UpdatedAt = DateTimeOffset.UtcNow;
        entity.IsDeleted = true;
        context.Set<T>().Update(entity);
    }

    public async Task<bool> AllIdsExistAsync(List<Guid> ids)
    {
        return await context.Set<T>().CountAsync(x => ids.Contains(x.Id)) == ids.Count;
    }

    public T AttachStub(Guid id)
    {
        // If the entity has been tracked by EF Core
        var tracked = context.Set<T>().Local.FirstOrDefault(x => x.Id == id);
        if (tracked is not null)
        {
            return tracked;
        }
        
        // Create a stub object (fake object that just has the ID) -> save memory
        if (typeof(T) == typeof(Criterion))
        {
            // Criterion is abstract, so we instantiate a concrete subclass (e.g., NumericCriterion)
            var criterionStub = new NumericCriterion { Id = id };
        
            // Attach to database set as Unchanged
            context.Set<Criterion>().Attach(criterionStub);
        
            return (T)(object)criterionStub;
        }
        
        var stub = Activator.CreateInstance<T>();
        stub.Id = id;
        context.Set<T>().Attach(stub);
        return stub;
    }

    public void UpdateRelations(ICollection<T> collection, IEnumerable<Guid>? ids)
    {
        if (ids == null) return;
        var newIds = ids.ToHashSet();

        // Remove items no longer in the list
        var toRemove = collection.Where(x => !newIds.Contains(x.Id)).ToList();
        foreach (var item in toRemove)
        {
            collection.Remove(item);
        }

        // Add new items only
        var existingIds = collection.Select(x => x.Id).ToHashSet();
        foreach (var id in newIds.Where(id => !existingIds.Contains(id)))
        {
            collection.Add(AttachStub(id));
        }
    }
}