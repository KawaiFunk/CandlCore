using Hangfire;
using Infrastructure.BackgroundJobs.CoinloreJob;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.BackgroundJobs;

public static class BackgroundJobsConfiguration
{
    public static void ConfigureJobs(IServiceProvider serviceProvider)
    {
        var recurringJobManager = serviceProvider.GetRequiredService<IRecurringJobManager>();
        var coinloreJob = serviceProvider.GetRequiredService<ICoinloreJob>();

        recurringJobManager.AddOrUpdate(
            "GetCryptoAssetsJob",
            () => coinloreJob.GetCryptoAssetsAsync(),
            Cron.Minutely
        );
    }
}