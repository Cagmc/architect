using Architect.Database.Entities;
using Architect.Database.Infrastructure;

namespace Architect.PersonFeature.Queries
{
    public class PersonQueries
    {
        private readonly EntityStore<Person, PersonAggregate> store;

        public PersonQueries(EntityStore<Person, PersonAggregate> store)
        {
            this.store = store;
        }
    }
}
