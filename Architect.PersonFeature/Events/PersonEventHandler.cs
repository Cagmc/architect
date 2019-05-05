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
        private readonly EntityStore<DatabaseContext, Person> store;

        public PersonEventHandler(DatabaseContext context, EntityStore<DatabaseContext, Person> store)
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
            var nameAggregate = new NameAggregate(person.Name, person.Id);
            var addressAggregate = new AddressAggregate(person.Address, person.Id);

            context.Set<PersonAggregate>().Add(aggregate);
            context.Set<NameAggregate>().Add(nameAggregate);
            context.Set<AddressAggregate>().Add(addressAggregate);
        }

        protected virtual async Task RemoveAsync(int id, CancellationToken token = default)
        {
            var original = await store.GetAggregateAsync<PersonAggregate>(id, token);
            var originalName = await store.GetAggregateAsync<NameAggregate>(id, token);
            var originalAddress = await store.GetAggregateAsync<AddressAggregate>(id, token);

            context.Set<PersonAggregate>().Remove(original);
            context.Set<NameAggregate>().Remove(originalName);
            context.Set<AddressAggregate>().Remove(originalAddress);
        }
    }
}
