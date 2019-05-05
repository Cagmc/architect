using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace Architect.Common.Infrastructure
{
    public abstract class EntityStore<TDbContext, TEntity, TAggregate>
        where TDbContext : DbContext
        where TEntity : EntityBase
        where TAggregate : AggregateEntityBase
    {
        protected readonly TDbContext context;

        public EntityStore(TDbContext context)
        {
            context.ArgumentNullCheck(nameof(context));

            this.context = context;
        }

        public abstract Task<TEntity> GetEntityAsync(int id, CancellationToken token = default);

        public abstract Task<TAggregate> GetAggregateAsync(int id, CancellationToken token = default);
    }
}
