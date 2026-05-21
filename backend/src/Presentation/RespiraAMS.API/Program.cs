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
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("AppConn"));
});
builder.Services.AddInfrastructure();
builder.Services.AddServices();
builder.Services.AddProfiles();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddExceptionHandler<ExceptionHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.Theme = ScalarTheme.Kepler;
    });
}

app.UseExceptionHandler(_ => { });

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();