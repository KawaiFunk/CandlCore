using Application.Interfaces.Clients.Coinlore;
using Application.Interfaces.Services;
using Common.Constants;
using Infrastructure.Clients.Coinlore;
using Infrastructure.Configurations;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings"));
builder.Services.Configure<CoinloreOptions>(
    builder.Configuration.GetSection("Coinlore"));

builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddScoped<AssetRepository>();
builder.Services.AddHttpClient("CoinloreClient");
builder.Services.AddSingleton<ICoinloreHttpClientFactory, CoinloreHttpClientFactory>();
builder.Services.AddScoped<ICoinloreClient, CoinloreClient>();
builder.Services.AddScoped<ICoinloreUrlBuilder, CoinloreUrlBuilder>();
builder.Services.AddScoped<ICoinloreService, CoinloreService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
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