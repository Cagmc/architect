namespace Architect.Common.Infrastructure
{
    public interface IEventDispatcher
    {
        void Dispatch(IEvent domainEvent);
    }
}
