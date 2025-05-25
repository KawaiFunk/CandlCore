using Domain.Interfaces.Repositories;
using Domain.Interfaces.Repositories.Generic;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Configurations.ServiceRegistrations;

public static class RepositoriesConfiguration
{
    public static void AddRepositoriesConfiguration(this IServiceCollection services)
    {
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IAssetRepository, AssetRepository>();
    }
}