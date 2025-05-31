using Application.Interfaces.Mediator;
using Infrastructure.Mediator;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Configurations.ServiceRegistrations;

public static class MediatorConfiguration
{
    public static void AddMediatorConfiguration(this IServiceCollection services)
    {
        services.AddScoped<IMediator, BaseMediator>();
        services.Scan(scan => scan
            .FromApplicationDependencies()
            .AddClasses(classes => classes.AssignableTo(typeof(IRequestHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
    }
}