using RespiraAMS.Domain.Models;

namespace RespiraAMS.Application.RepositoryContracts;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<T> Repo<T>() where T : Base;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task ExecuteInTransactionAsync(Func<Task> action, CancellationToken token = default);
    Task ExecuteInTransactionAsync(Action action, CancellationToken token = default);
}