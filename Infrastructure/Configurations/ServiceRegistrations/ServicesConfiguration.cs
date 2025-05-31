using Application.Interfaces.Clients.Coinlore;
using Application.Interfaces.Services;
using Infrastructure.Clients.Coinlore;
using Infrastructure.Services.Asset;
using Microsoft.Extensions.Caching.Memory;
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

        services.AddScoped<AssetService>();
        services.AddScoped<IAssetService>(provider =>
            new CacheAssetService(
                provider.GetRequiredService<AssetService>(),
                provider.GetRequiredService<IMemoryCache>()));
    }
}