using System;
using System.Threading;
using System.Threading.Tasks;

namespace Architect.Database.Infrastructure
{
    public abstract class EntityStore<T> where T: EntityBase
    {
        protected readonly DatabaseContext context;

        public EntityStore(DatabaseContext context)
        {
            context.ArgumentNullCheck(nameof(context));

            this.context = context;
        }

        public abstract Task<T> GetEntityAsync(int id, CancellationToken token = default);
    }
}
