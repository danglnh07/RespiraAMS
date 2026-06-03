using System.Text.Json;
using System.Text.Json.Serialization;
using BuildingBlocks.Middlewares;
using RespiraAMS.Application;
using RespiraAMS.Application.Features.AntibioticSpectra.CreateAntibioticSpectrum;
using RespiraAMS.Application.Features.AntibioticSpectra.DeleteAntibioticSpectrum;
using RespiraAMS.Application.Features.AntibioticSpectra.GetPagedAntibioticSpectrum;
using RespiraAMS.Application.Features.AntibioticSpectra.UpdateAntibioticSpectrum;
using RespiraAMS.Infrastructure;
using Scalar.AspNetCore;
using Wolverine;
using Wolverine.EntityFrameworkCore;
using Wolverine.FluentValidation;
using Wolverine.Postgresql;

var builder = WebApplication.CreateBuilder(args);

// Configure controllers
builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(
            namingPolicy: JsonNamingPolicy.CamelCase,
            allowIntegerValues: false));
    });
builder.Services.AddOpenApi();
builder.Services.AddProfiles();
builder.Services.AddFluentValidators();
builder.Services.AddExceptionHandler<ExceptionHandler>();
var origins = builder.Configuration.GetSection("CORS").Get<string[]>();
if (origins is null || origins.Length == 0)
{
    origins = ["*"];
}
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.WithOrigins(origins)
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
builder.AddInfrastructure();
builder.Host.UseWolverine(opts =>
{
    opts.RestoreV5Defaults();
    opts.Discovery.IncludeAssembly(typeof(CreateAntibioticSpectrumHandler).Assembly);
    opts.Discovery.IncludeAssembly(typeof(GetPagedAntibioticSpectrumHandler).Assembly);
    opts.Discovery.IncludeAssembly(typeof(UpdateAntibioticSpectrumHandler).Assembly);
    opts.Discovery.IncludeAssembly(typeof(DeleteAntibioticSpectrumHandler).Assembly);

    var connectionString = builder.Configuration.GetConnectionString("AppConn") ??
                           throw new InvalidOperationException("No connection string for app db");

    opts.PersistMessagesWithPostgresql(connectionString, "app_db");
    opts.UseEntityFrameworkCoreTransactions();

    opts.UseFluentValidation(RegistrationBehavior.ExplicitRegistration);
    
    opts.Durability.Mode = DurabilityMode.Solo;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options => { options.Theme = ScalarTheme.Kepler; });
}

app.UseCors("AllowSpecificOrigin");

app.UseExceptionHandler(_ => { });

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Seed data
await app.SeedData();

app.Run();