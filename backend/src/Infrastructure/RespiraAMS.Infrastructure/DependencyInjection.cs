using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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

    public static async Task SeedData(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var provider = scope.ServiceProvider;
        var context = provider.GetRequiredService<AppDbContext>();
        var logger = provider.GetRequiredService<ILogger<DbInitializer>>();
        await DbInitializer.InitializeAsync(context, logger);
    }
}