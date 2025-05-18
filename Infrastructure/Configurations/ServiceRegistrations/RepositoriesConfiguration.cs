using Domain.Interfaces.Repositories;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Configurations.ServiceRegistrations;

public static class RepositoriesConfiguration
{
    public static void AddRepositoriesConfiguration(this IServiceCollection services)
    {
        services.AddScoped<IAssetRepository, AssetRepository>();
    }
}