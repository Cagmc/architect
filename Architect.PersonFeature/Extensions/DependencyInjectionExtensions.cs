using Architect.Database.Entities;
using Architect.Database.Infrastructure;
using Architect.PersonFeature;
using Architect.PersonFeature.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddPeopleFeature(this IServiceCollection services)
        {
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IPersonTransactionalService, PersonTransactionalService>();
            services.AddScoped<EntityStore<Person>, PersonStore>();
            services.AddScoped<PersonStore, PersonStore>();

            return services;
        }
    }
}
