using System;

using Microsoft.EntityFrameworkCore;

namespace Architect.Common.Infrastructure
{
    public abstract class DomainServiceBase<TDbContext, TEntity, TAggregate> : ResponsiveServiceBase<TEntity, TDbContext>
        where TDbContext : DbContext
        where TEntity : EntityBase
        where TAggregate : AggregateEntityBase
    {
        protected readonly IEventDispatcher eventDispatcher;
        protected readonly EntityStore<TDbContext, TEntity> store;

        public DomainServiceBase(TDbContext context,
            EntityStore<TDbContext, TEntity> store, IEventDispatcher eventDispatcher) : base(context)
        {
            this.eventDispatcher = eventDispatcher.ArgumentNullCheck(nameof(eventDispatcher));
            this.store = store.ArgumentNullCheck(nameof(store));
        }
    }
}
