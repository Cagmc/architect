using System;
using System.Threading;
using System.Threading.Tasks;

using Architect.Common.Infrastructure;

namespace Architect.PersonFeature.Events
{
    public class PersonEventHandler : 
        IEventHandler<CreateEvent>,
        IEventHandler<UpdateEvent>
    {
        private readonly PersonDatabaseContext context;

        public PersonEventHandler(PersonDatabaseContext context)
        {
            this.context = context;
        }

        public async Task HandleAsync(CreateEvent command, CancellationToken token = default)
        {
            var aggregate = new Entities.PersonAggregate(command.Person);

            context.People.Add(aggregate);

            await context.SaveChangesAsync(token);
        }

        public Task HandleAsync(UpdateEvent command, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }
    }
}
