using Application.Mappers.AssetProfile;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Configurations;

public static class MappersConfigurations
{
    public static void AddMappersConfiguration(this IServiceCollection services)
    {
        services.AddScoped<IAssetMapper, AssetMapper>();
    }
}