using Application.Configurations;
using Application.Interfaces.Clients.Coinlore;
using Application.Interfaces.Services;
using Common.Constants;
using Hangfire;
using Hangfire.Mongo;
using Hangfire.Mongo.Migration.Strategies;
using Hangfire.Mongo.Migration.Strategies.Backup;
using Infrastructure.BackgroundJobs;
using Infrastructure.Clients.Coinlore;
using Infrastructure.Configurations;
using Infrastructure.Configurations.ServiceRegistrations;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddOpenApi();

//Hangfire
builder.Services.AddHangfireServices(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//In memory cache
builder.Services.AddMemoryCache();
//Serilog
SerilogConfiguration.AddSerilogConfiguration(builder.Configuration);
//MongoDb
builder.Services.AddMongoDbConfiguration(builder.Configuration);
//Options
builder.Services.AddOptionsConfiguration(builder.Configuration);
//Services
builder.Services.AddServicesConfiguration();
//Repositories
builder.Services.AddRepositoriesConfiguration();
//Jobs
builder.Services.AddJobsConfiguration();
//Mappers
builder.Services.AddMappersConfiguration();
//Mediator
builder.Services.AddMediatorConfiguration();


var app = builder.Build();

//Add hangfire jobs
using (var scope = app.Services.CreateScope())
{
    BackgroundJobsConfiguration.ConfigureJobs(scope.ServiceProvider);
}

app.UseHangfireDashboard();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();