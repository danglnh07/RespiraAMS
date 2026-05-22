using Microsoft.EntityFrameworkCore.Storage;
using RespiraAMS.Application.RepositoryContracts;
using RespiraAMS.Domain.Models;
using RespiraAMS.Infrastructure.Data;

namespace RespiraAMS.Infrastructure.Repositories;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    private IExecutionStrategy GetExecutionStrategy() => context.Database.CreateExecutionStrategy();
    private readonly Dictionary<string, object> _repos = [];

    public void Dispose()
    {
        context.Dispose();
        GC.SuppressFinalize(this);
    }

    public IGenericRepository<T> Repo<T>() where T : Base
    {
        var type = typeof(T).Name;

        // Get cached repository
        if (_repos.TryGetValue(type, out var cachedRepo)) return (IGenericRepository<T>)cachedRepo;

        // If the type request has specific implementation
        if (typeof(T).IsAssignableTo(typeof(Criterion)))
        {
            var criterionRepo = new CriterionRepository(context);
            _repos.Add(type, criterionRepo);
            return (IGenericRepository<T>)criterionRepo;
        }

        if (typeof(T).IsAssignableTo(typeof(TreatmentProtocol)))
        {
            var treatmentProtocolRepo = new TreatmentProtocolRepository(context);
            _repos.Add(type, treatmentProtocolRepo);
            return (IGenericRepository<T>)treatmentProtocolRepo;
        }

        // If the model use GenericRepo
        var repo = new GenericRepository<T>(context);
        _repos.Add(type, repo);
        return repo;
    }

    public async Task ExecuteInTransactionAsync(Func<Task> action, CancellationToken token)
    {
        var strategy = GetExecutionStrategy();
        await strategy.ExecuteAsync(
            action,
            async (ctx, op, cancellationToken) =>
            {
                await using var transaction = await ctx.Database
                    .BeginTransactionAsync(cancellationToken);

                try
                {
                    await op();
                    await ctx.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync(cancellationToken);
                }
                catch
                {
                    await transaction.RollbackAsync(cancellationToken);
                    throw;
                }

                return true;
            },
            null,
            token);
    }

    public async Task ExecuteInTransactionAsync(Action action, CancellationToken token = default)
    {
        var strategy = GetExecutionStrategy();
        await strategy.ExecuteAsync(
            action,
            async (ctx, op, cancellationToken) =>
            {
                await using var transaction = await ctx.Database
                    .BeginTransactionAsync(cancellationToken);

                try
                {
                    op();
                    await ctx.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync(cancellationToken);
                }
                catch
                {
                    await transaction.RollbackAsync(cancellationToken);
                    throw;
                }

                return true;
            },
            null,
            token);
    }

    public async Task<int> SaveChangesAsync(CancellationToken ct)
    {
        var strategy = GetExecutionStrategy();
        return await strategy.ExecuteAsync(context, (_, state, token) => state.SaveChangesAsync(token), null, ct);
    }
}