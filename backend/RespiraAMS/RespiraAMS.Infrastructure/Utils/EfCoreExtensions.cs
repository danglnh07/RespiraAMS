using RespiraAMS.Core.Models;
using RespiraAMS.Core.RepositoryContract;

namespace RespiraAMS.Infrastructure.Utils;

public static class EfCoreExtensions
{
    public static void UpdateRelations<TEntity>(this ICollection<TEntity> collection, IEnumerable<Guid>? ids,
        IGenericRepository<TEntity> repo) where TEntity : Base
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
            collection.Add(repo.AttachStub(id));
        }
    }
}