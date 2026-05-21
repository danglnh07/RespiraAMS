using System.Text.Json.Serialization;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using RespiraAMS.API.Middleware;
using RespiraAMS.Core.Validators;
using RespiraAMS.Infrastructure;
using RespiraAMS.Infrastructure.Data;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using Scalar.AspNetCore;

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
builder.Services.AddValidatorsFromAssemblyContaining<AntibioticValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<AntibioticSpectrumValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CriterionValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<DiseasePathogenValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<DiseaseValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<IcuHospitalizeCriterionValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<PathogenValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<ResistanceRiskFactorValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<TreatmentProtocolValidator>();
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