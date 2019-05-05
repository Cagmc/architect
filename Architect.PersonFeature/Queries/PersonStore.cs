using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Architect.Common.Infrastructure;
using Architect.Database;
using Architect.Database.Entities;

using Microsoft.EntityFrameworkCore;

namespace Architect.PersonFeature.Queries
{
    public class PersonStore : EntityStore<DatabaseContext, Person>
    {
        public PersonStore(DatabaseContext context) : base(context)
        {

        }

        public override async Task<Person> GetEntityAsync(int id, CancellationToken token = default)
        {
            var entity = await context.Set<Person>()
                .Where(x => x.Id == id)
                .Include(x => x.Name)
                .Include(x => x.Address)
                .SingleOrDefaultAsync(token);

            return entity;
        }

        public override async Task<TAggregate> GetAggregateAsync<TAggregate>(int id, CancellationToken token = default)
        {
            var aggregate = await context.Set<TAggregate>()
                .Where(x => x.AggregateRootId == id)
                .SingleOrDefaultAsync(token);

            return aggregate;
        }
    }
}
