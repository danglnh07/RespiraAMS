using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using RespiraAMS.API.Middleware;
using RespiraAMS.Application;
using RespiraAMS.Infrastructure;
using RespiraAMS.Infrastructure.Data;
using Scalar.AspNetCore;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(
            namingPolicy: JsonNamingPolicy.CamelCase,
            allowIntegerValues: false));
    });
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("AppConn"));
});
builder.Services.AddInfrastructure();
builder.Services.AddServices();
builder.Services.AddProfiles();
builder.Services.AddFluentValidators();
builder.Services.AddFluentValidationAutoValidation();
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

// Run database seeding
using (var scope = app.Services.CreateScope())
{
    var provider = scope.ServiceProvider;
    var context = provider.GetRequiredService<AppDbContext>();
    var logger = provider.GetRequiredService<ILogger<DbInitializer>>();
    await DbInitializer.InitializeAsync(context, logger);
}

app.Run();