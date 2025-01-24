using Microsoft.Extensions.DependencyInjection;

namespace OptimizelyDeleteMissingCmsProperties;

public static class ServiceExtensions
{
    public static IServiceCollection AddDeleteMissingCmsProperties(this IServiceCollection services)
    {
        services.AddTransient<IMissingCmsPropertiesService, MissingCmsPropertiesService>();
        services.AddTransient<DeleteMissingCmsPropertiesScheduledJob>();
        services.AddTransient<ListMissingCmsPropertiesScheduledJob>();

        return services;
    }
}