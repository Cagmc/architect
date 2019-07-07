using Architect.UserFeature.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddUserFeature(this IServiceCollection services)
        {
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<ILoginTransactionalService, LoginTransactionalService>();
            services.AddScoped<IRegistrationService, RegistrationService>();
            services.AddScoped<IRegistrationTransactionalService, RegistrationTransactionalService>();

            return services;
        }
    }
}
