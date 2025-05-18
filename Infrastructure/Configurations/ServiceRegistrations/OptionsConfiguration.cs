using Common.Constants;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Configurations.ServiceRegistrations;

public static class OptionsConfiguration
{
    public static void AddOptionsConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<CoinloreOptions>(
            configuration.GetSection("Coinlore"));
    }
}