using System;
using System.Threading;
using System.Threading.Tasks;

using Architect.Common.Infrastructure;

namespace Architect.Database.Infrastructure
{
    public abstract class EntityStore<TEntity, TAggregate>
        where TEntity : EntityBase
        where TAggregate : AggregateEntityBase
    {
        protected readonly DatabaseContext context;

        public EntityStore(DatabaseContext context)
        {
            context.ArgumentNullCheck(nameof(context));

            this.context = context;
        }

        public abstract Task<TEntity> GetEntityAsync(int id, CancellationToken token = default);

        public abstract Task<TAggregate> GetAggregateAsync(int id, CancellationToken token = default);
    }
}
