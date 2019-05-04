using System;
using System.Threading;
using System.Threading.Tasks;

using Architect.Common.Infrastructure;
using Architect.Common.Infrastructure.DataTransfer.Response;
using Architect.Database.Entities;
using Architect.Database.Infrastructure;
using Architect.PersonFeature.DataTransfer.Request;

using Microsoft.EntityFrameworkCore;

namespace Architect.PersonFeature.Queries
{
    public class PersonQueries : ResponsiveServiceBase<Person>, IPersonQueries
    {
        private readonly Database.DatabaseContext context;
        private readonly EntityStore<Person, PersonAggregate> store;

        public PersonQueries(Database.DatabaseContext context, EntityStore<Person, PersonAggregate> store)
        {
            this.context = context.ArgumentNullCheck(nameof(context));
            this.store = store.ArgumentNullCheck(nameof(store));
        }

        public virtual async Task<IDataResponse<PersonAggregate>> GetAsync(
            int id, CancellationToken token = default)
        {
            if (id < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            var item = await store.GetAggregateAsync(id, token);

            var response = item == null
                ? base.NotFoundDataResponse<PersonAggregate>(id)
                : new DataResponse<PersonAggregate>(item, id);

            return response;
        }

        public virtual async Task<IListResponse<PersonAggregate>> GetAsync(
            PeopleFilter filter, CancellationToken token = default)
        {
            filter.ArgumentNullCheck(nameof(filter));

            var query = context.PersonAggregates
                .StringFilter(filter.Filter, x => x.Name, x => x.Address);

            var totalCount = await query.CountAsync(token);
            var items = await query.Paginate(filter).ToListAsync(token);

            return new ListResponse<PersonAggregate>(items, totalCount,
                filter.Page ?? 1, totalCount.CountPages(filter.PageSize));
        }
    }
}
