using System;

using Architect.Common.Infrastructure;

namespace Architect.Database.Infrastructure
{
    public abstract class ServiceBase<TEntity, TAggregate> : ResponsiveServiceBase<TEntity>
        where TEntity : EntityBase
        where TAggregate : AggregateEntityBase
    {
        protected readonly IEventDispatcher eventDispatcher;
        protected readonly DatabaseContext context;
        protected readonly EntityStore<TEntity, TAggregate> store;

        public ServiceBase(DatabaseContext context,
            EntityStore<TEntity, TAggregate> store, IEventDispatcher eventDispatcher)
        {
            this.eventDispatcher = eventDispatcher.ArgumentNullCheck(nameof(eventDispatcher));
            this.context = context.ArgumentNullCheck(nameof(context));
            this.store = store.ArgumentNullCheck(nameof(store));
        }
    }
}
