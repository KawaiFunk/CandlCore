using Hangfire;
using Hangfire.Mongo;
using Hangfire.Mongo.Migration.Strategies;
using Hangfire.Mongo.Migration.Strategies.Backup;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Configurations.ServiceRegistrations
{
    public static class HangfireConfiguration
    {
        public static void AddHangfireServices(this IServiceCollection services, IConfiguration configuration)
        {
            var mongoConnectionString = configuration.GetConnectionString("MongoDb");

            var mongoStorageOptions = new MongoStorageOptions
            {
                MigrationOptions = new MongoMigrationOptions
                {
                    MigrationStrategy = new MigrateMongoMigrationStrategy(),
                    BackupStrategy = new CollectionMongoBackupStrategy()
                },
                CheckConnection = true,
                CheckQueuedJobsStrategy = CheckQueuedJobsStrategy.Poll
            };

            services.AddHangfire(config =>
            {
                config.UseMongoStorage(mongoConnectionString, mongoStorageOptions);
            });

            services.AddHangfireServer();
        }
    }
}