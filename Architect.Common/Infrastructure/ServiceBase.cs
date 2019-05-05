using System;

using Microsoft.EntityFrameworkCore;

namespace Architect.Common.Infrastructure
{
    public abstract class ServiceBase<TDbContext, TEntity, TAggregate> : ResponsiveServiceBase<TEntity>
        where TDbContext : DbContext
        where TEntity : EntityBase
        where TAggregate : AggregateEntityBase
    {
        protected readonly IEventDispatcher eventDispatcher;
        protected readonly TDbContext context;
        protected readonly EntityStore<TDbContext, TEntity> store;

        public ServiceBase(TDbContext context,
            EntityStore<TDbContext, TEntity> store, IEventDispatcher eventDispatcher)
        {
            this.eventDispatcher = eventDispatcher.ArgumentNullCheck(nameof(eventDispatcher));
            this.context = context.ArgumentNullCheck(nameof(context));
            this.store = store.ArgumentNullCheck(nameof(store));
        }
    }
}
