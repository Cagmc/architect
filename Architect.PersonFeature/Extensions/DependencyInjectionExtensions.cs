using Architect.Common.Infrastructure;
using Architect.Database;
using Architect.Database.Entities;
using Architect.PersonFeature.Queries;
using Architect.PersonFeature.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddPeopleFeature(this IServiceCollection services)
        {
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IPersonTransactionalService, PersonTransactionalService>();
            services.AddScoped<IPersonQueries, PersonQueries>();
            services.AddScoped<EntityStore<DatabaseContext, Person>, PersonStore>();
            services.AddScoped<PersonStore, PersonStore>();

            return services;
        }
    }
}
