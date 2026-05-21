using Microsoft.Extensions.DependencyInjection;
using RespiraAMS.Domain.RepositoryContracts;
using RespiraAMS.Infrastructure.Repositories;

namespace RespiraAMS.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}