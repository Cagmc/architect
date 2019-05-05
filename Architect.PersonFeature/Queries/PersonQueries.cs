using System;
using System.Threading;
using System.Threading.Tasks;

using Architect.Common.Infrastructure;
using Architect.Common.Infrastructure.DataTransfer.Response;
using Architect.Database.Entities;
using Architect.PersonFeature.DataTransfer.Request;

using Microsoft.EntityFrameworkCore;

namespace Architect.PersonFeature.Queries
{
    public class PersonQueries : QueryServiceBase<Database.DatabaseContext, Person>, IPersonQueries
    {
        public PersonQueries(Database.DatabaseContext context) :base(context)
        {
        }

        public async Task<IDataResponse<AddressAggregate>> GetAddressAsync(
            int id, CancellationToken token = default)
        {
            var response = await GetAggregateAsync<AddressAggregate>(id, token);

            return response;
        }

        public async Task<IListResponse<AddressAggregate>> GetAddressAsync(
            AddressFilter filter, CancellationToken token = default)
        {
            filter.ArgumentNullCheck(nameof(filter));

            var query = context.Query<AddressAggregate>()
                .KeyFilter(filter.Id, x => x.Id)
                .StringFilter(filter.Filter, x => x.Address, x => x.Country);

            var totalCount = await query.CountAsync(token);
            var items = await query.Paginate(filter).ToListAsync(token);

            return new ListResponse<AddressAggregate>(items, totalCount,
                filter.Page ?? 1, totalCount.CountPages(filter.PageSize));
        }


        public virtual async Task<IDataResponse<PersonAggregate>> GetAsync(
            int id, CancellationToken token = default)
        {
            var response = await GetAggregateAsync<PersonAggregate>(id, token);

            return response;
        }

        public virtual async Task<IListResponse<PersonAggregate>> GetAsync(
            PeopleFilter filter, CancellationToken token = default)
        {
            filter.ArgumentNullCheck(nameof(filter));

            var query = context.Query<PersonAggregate>()
                .StringFilter(filter.Filter, x => x.Name, x => x.Address);

            var totalCount = await query.CountAsync(token);
            var items = await query.Paginate(filter).ToListAsync(token);

            return new ListResponse<PersonAggregate>(items, totalCount,
                filter.Page ?? 1, totalCount.CountPages(filter.PageSize));
        }

        public async Task<IDataResponse<NameAggregate>> GetNameAsync(
            int id, CancellationToken token = default)
        {
            var response = await GetAggregateAsync<NameAggregate>(id, token);

            return response;
        }

        public async Task<IListResponse<NameAggregate>> GetNameAsync(
            NameFilter filter, CancellationToken token = default)
        {
            filter.ArgumentNullCheck(nameof(filter));

            var query = context.Query<NameAggregate>()
                .KeyFilter(filter.Id, x => x.Id)
                .StringFilter(filter.Filter, x => x.ShortName, x => x.LongName);

            var totalCount = await query.CountAsync(token);
            var items = await query.Paginate(filter).ToListAsync(token);

            return new ListResponse<NameAggregate>(items, totalCount,
                filter.Page ?? 1, totalCount.CountPages(filter.PageSize)); throw new NotImplementedException();
        }
    }
}
