using System.Threading;
using System.Threading.Tasks;

using Architect.Common.Infrastructure;
using Architect.Database;
using Architect.Database.Entities;

namespace Architect.PersonFeature.Events
{
    public class PersonEventHandler : 
        IEventHandler<CreateEvent>,
        IEventHandler<UpdateEvent>,
        IEventHandler<DeleteEvent>
    {
        private readonly DatabaseContext context;
        private readonly EntityStore<DatabaseContext, Person, PersonAggregate> store;

        public PersonEventHandler(DatabaseContext context, EntityStore<DatabaseContext, Person, PersonAggregate> store)
        {
            this.context = context;
            this.store = store;
        }

        public virtual async Task HandleAsync(CreateEvent data, CancellationToken token = default)
        {
            Add(data.Person);

            await context.SaveChangesAsync(token);
        }

        public virtual async Task HandleAsync(DeleteEvent data, CancellationToken token = default)
        {
            await RemoveAsync(data.Person.Id, token);

            await context.SaveChangesAsync(token);
        }

        public virtual async Task HandleAsync(UpdateEvent data, CancellationToken token = default)
        {
            await RemoveAsync(data.Person.Id, token);
            Add(data.Person);

            await context.SaveChangesAsync(token);
        }

        protected virtual void Add(Person person)
        {
            var aggregate = new PersonAggregate(person);
            context.PersonAggregates.Add(aggregate);
        }

        protected virtual async Task RemoveAsync(int id, CancellationToken token = default)
        {
            var original = await store.GetAggregateAsync(id, token);
            context.PersonAggregates.Remove(original);
        }
    }
}
