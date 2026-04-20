
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using studyRats.Service.Platform.Api.Infrastructure;
using studyRats.Service.Platform.Application;
using studyRats.Service.Platform.Data;
using studyRats.Service.Platform.Data.Repositories;
using studyRats.Service.Platform.Domain.Abstractions;
using studyRats.Service.Platform.Domain.Abstractions.Repositories;

// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, World!");


var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    ContentRootPath = Directory.GetCurrentDirectory()
});

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

// Add services to the Container

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        // This is the interjection!
        options.InvalidModelStateResponseFactory = ModelStateValidator.ValidateModelState;
    });

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Database
var connectionString = builder.Configuration.GetConnectionString("DevDatabase");

if (string.IsNullOrEmpty(connectionString))
{
    throw new Exception("Could not find the 'DevDatabase' connection string. Check appsettings.json!");
}

builder.Services.AddDbContext<DataContext>(options => options.UseNpgsql(connectionString));

// MediatR
builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssembly(typeof(Program).Assembly);
    configuration.RegisterServicesFromAssembly(typeof(MediatrDI).Assembly);
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();



var app = builder.Build();

// Swagger
app.UseSwagger();
    // Sets Swagger to load at the root URL (localhost:port/) instead of (localhost:port/swagger)
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "studyRats API v1");
        c.RoutePrefix = string.Empty;
    });


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();