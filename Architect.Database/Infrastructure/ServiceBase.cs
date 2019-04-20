namespace Architect.Database.Infrastructure
{
    public abstract class ServiceBase<T> where T: EntityBase
    {
        protected readonly DatabaseContext context;
        protected readonly EntityStore<T> store;

        public ServiceBase(DatabaseContext context, EntityStore<T> store)
        {
            this.context = context;
            this.store = store;
        }
    }
}
