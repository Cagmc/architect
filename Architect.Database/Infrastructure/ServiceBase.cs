using System;

using Architect.Common.Infrastructure;

namespace Architect.Database.Infrastructure
{
    public abstract class ServiceBase<T> where T: EntityBase
    {
        protected readonly IEventDispatcher eventDispatcher;
        protected readonly DatabaseContext context;
        protected readonly EntityStore<T> store;

        public ServiceBase(DatabaseContext context, EntityStore<T> store, IEventDispatcher eventDispatcher)
        {
            this.eventDispatcher = eventDispatcher.ArgumentNullCheck(nameof(eventDispatcher));
            this.context = context.ArgumentNullCheck(nameof(context));
            this.store = store.ArgumentNullCheck(nameof(store));
        }
    }
}
