using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RespiraAMS.Application.Abstracts.Data;
using RespiraAMS.Infrastructure.Data;

namespace RespiraAMS.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IHostApplicationBuilder builder)
    {
        builder.AddNpgsqlDbContext<AppDbContext>("AppConn");
        builder.Services.AddScoped<IDbContext, AppDbContext>();
    }
}