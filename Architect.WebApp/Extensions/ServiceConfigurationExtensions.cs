using Architect.Common.Infrastructure;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceConfigurationExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddPeopleFeature();
            services.AddUserFeature();
            services.AddScoped<IEventDispatcher, EventDispatcher>();

            return services;
        }
    }
}
