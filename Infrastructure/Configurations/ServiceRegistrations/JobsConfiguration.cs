using Infrastructure.BackgroundJobs.CoinloreJob;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Configurations.ServiceRegistrations;

public static class JobsConfiguration
{
    public static void AddJobsConfiguration(this IServiceCollection services)
    {
        services.AddScoped<ICoinloreJob, CoinloreJob>();
    }
}