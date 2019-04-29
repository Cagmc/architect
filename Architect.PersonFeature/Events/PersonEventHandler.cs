using System;
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

        public PersonEventHandler(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task HandleAsync(CreateEvent command, CancellationToken token = default)
        {
            var aggregate = new PersonAggregate(command.Person);

            context.PersonAggregates.Add(aggregate);

            await context.SaveChangesAsync(token);
        }

        public Task HandleAsync(DeleteEvent command, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public Task HandleAsync(UpdateEvent command, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }
    }
}
