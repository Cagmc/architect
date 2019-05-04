using System;
using System.Threading;
using System.Threading.Tasks;

using Architect.Common.Infrastructure;
using Architect.Common.Infrastructure.DataTransfer.Request;
using Architect.Common.Infrastructure.DataTransfer.Response;
using Architect.Database.Entities;
using Architect.Database.Infrastructure;

using Microsoft.EntityFrameworkCore;

namespace Architect.PersonFeature.Queries
{
    public class PersonQueries : ResponsiveServiceBase<Person>
    {
        private readonly Database.DatabaseContext context;
        private readonly EntityStore<Person, PersonAggregate> store;

        public PersonQueries(Database.DatabaseContext context, EntityStore<Person, PersonAggregate> store)
        {
            this.context = context;
            this.store = store;
        }

        public virtual async Task<IDataResponse<PersonAggregate>> GetAsync(
            int id, CancellationToken token = default)
        {
            var item = await store.GetAggregateAsync(id, token);

            var response = item == null 
                ? base.NotFoundDataResponse<PersonAggregate>(id) 
                : new DataResponse<PersonAggregate>(item, id);

            return response;
        }

        public virtual async Task<IListResponse<PersonAggregate>> GetAsync(
            PaginationFilter filter, CancellationToken token = default)
        {
            var query = context.PersonAggregates;

            var totalCount = await query.CountAsync(token);
            var items = await query.Paginate(filter).ToListAsync(token);

            return new ListResponse<PersonAggregate>(items, totalCount, totalCount.CountPages(filter.PageSize));
        }
    }
}
