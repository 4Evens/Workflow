using Application;
using FluentValidation.AspNetCore;
using Infrastructure;
using Microsoft.OpenApi.Models;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// 1. Infrastructure services (AutoMapper, Redis, RabbitMQ, etc.)
builder.Services.AddInfrastructureServices(builder.Configuration);

// 2. Application services (Business logic services)
builder.Services.AddApplicationServices();

// 3. Add Controllers (API endpoints) and FluentValidation integration
builder.Services.AddControllers()
    .AddFluentValidation(fv =>
    {
        // FluentValidation'ý otomatik tarayýcý olarak ayarlýyoruz
        fv.RegisterValidatorsFromAssemblyContaining<Program>();
        // Validator hata mesajlarýný otomatize etmek için 
        fv.DisableDataAnnotationsValidation = true;
    });

// 4. Configure Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Workflow API",
        Description = "An API for managing workflows, steps, approvals, and evaluations",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "API Support",
            Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "Use under LICX",
            Url = new Uri("https://example.com/license")
        }
    });
});

// 5. Register Persistence services (Repositories, etc.)
builder.Services.AddPersistenceServices(builder.Configuration);

// Build the application
var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Workflow API v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Map API controllers
app.MapControllers();

app.Run();
