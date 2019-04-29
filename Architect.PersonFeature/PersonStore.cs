using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Architect.Database.Entities;

using Microsoft.EntityFrameworkCore;

namespace Architect.PersonFeature
{
    public class PersonStore : Database.Infrastructure.EntityStore<Person, PersonAggregate>
    {
        public PersonStore(Database.DatabaseContext context) : base(context)
        {

        }

        public override async Task<Person> GetEntityAsync(int id, CancellationToken token = default)
        {
            var entity = await context.People
                .Where(x => x.Id == id)
                .Include(x => x.Name)
                .Include(x => x.Address)
                .SingleOrDefaultAsync(token);

            return entity;
        }

        public override async Task<PersonAggregate> GetAggregateAsync(int id, CancellationToken token = default)
        {
            var aggregate = await context.PersonAggregates
                .Where(x => x.AggregateRootId == id)
                .SingleOrDefaultAsync(token);

            return aggregate;
        }
    }
}
