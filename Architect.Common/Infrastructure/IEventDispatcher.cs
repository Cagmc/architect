using System.Threading;
using System.Threading.Tasks;

namespace Architect.Common.Infrastructure
{
    public interface IEventDispatcher
    {
        /// <summary>
        /// Awaitable event
        /// </summary>
        /// <typeparam name="T">Type of the in parameter</typeparam>
        /// <param name="domainEvent">Event data</param>
        /// <param name="token">Canc token</param>
        /// <returns></returns>
        Task DispatchAsync<T>(T domainEvent, CancellationToken token = default) where T: IEvent;

        /// <summary>
        /// Fire and forget event
        /// </summary>
        /// <typeparam name="T">Type of the in parameter</typeparam>
        /// <param name="domainEvent">Event data</param>
        void Dispatch<T>(T domainEvent) where T : IEvent;
    }
}
