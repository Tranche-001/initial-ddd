
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using studyRats.Service.Platform.Data;

// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, World!");


var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("DevDatabase");

builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer())