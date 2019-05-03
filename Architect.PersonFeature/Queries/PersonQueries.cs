using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Architect.Common.Infrastructure.DataTransfer.Request;
using Architect.Database.Entities;
using Architect.Database.Infrastructure;

using Microsoft.EntityFrameworkCore;

namespace Architect.PersonFeature.Queries
{
    public class PersonQueries
    {
        private readonly Database.DatabaseContext context;
        private readonly EntityStore<Person, PersonAggregate> store;

        public PersonQueries(Database.DatabaseContext context, EntityStore<Person, PersonAggregate> store)
        {
            this.context = context;
            this.store = store;
        }

        public virtual async Task<object> GetAsync(int id, CancellationToken token = default)
        {
            var result = await store.GetAggregateAsync(id, token);

            return result;
        }

        public virtual async Task<object> GetAsync(PaginationFilter filter, CancellationToken token = default)
        {
            var query = context.PersonAggregates
                .Take(filter.PageSize.Value);

            var items = await query.ToListAsync(token);

            return items;
        }
    }
}
