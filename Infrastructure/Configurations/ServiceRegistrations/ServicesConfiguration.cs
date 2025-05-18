using Application.Interfaces.Clients.Coinlore;
using Application.Interfaces.Services;
using Infrastructure.Clients.Coinlore;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Configurations.ServiceRegistrations;

public static class ServicesConfiguration
{
    public static void AddServicesConfiguration(this IServiceCollection services)
    {
        services.AddHttpClient("CoinloreClient");
        services.AddSingleton<ICoinloreHttpClientFactory, CoinloreHttpClientFactory>();
        services.AddScoped<ICoinloreClient, CoinloreClient>();
        services.AddScoped<ICoinloreUrlBuilder, CoinloreUrlBuilder>();
        services.AddScoped<ICoinloreService, CoinloreService>();
    }
}