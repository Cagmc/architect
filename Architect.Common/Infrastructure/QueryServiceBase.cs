using System;
using System.Threading;
using System.Threading.Tasks;

using Architect.Common.Infrastructure.DataTransfer.Response;

using Microsoft.EntityFrameworkCore;

namespace Architect.Common.Infrastructure
{
    public abstract class QueryServiceBase<TDbContext, TEntity> : ResponsiveServiceBase<TEntity>
        where TDbContext : DbContext
        where TEntity : class
    {
        protected readonly TDbContext context;

        public QueryServiceBase(TDbContext context)
        {
            this.context = context.ArgumentNullCheck(nameof(context));
        }

        protected virtual async Task<IDataResponse<TAggregate>> GetAggregateAsync<TAggregate>(
            int id, CancellationToken token = default)
            where TAggregate : AggregateEntityBase
        {
            id.ArgumentOutOfRangeCheck(nameof(id));

            var query = context.Set<TAggregate>();

            if (query == null)
            {
                throw new InvalidOperationException($"context.Set<T> where T is {typeof(TAggregate).FullName}");
            }

            var item = await query
                .KeyFilter(id, x => x.AggregateRootId)
                .SingleOrDefaultAsync(token);

            var response = item == null
                ? base.NotFoundDataResponse<TAggregate>(id)
                : new DataResponse<TAggregate>(item, id);

            return response;
        }
    }
}
