using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Architect.Common.Infrastructure
{
    public class EventDispatcher : IEventDispatcher
    {
        public virtual async Task DispatchAsync<T>(T domainEvent, CancellationToken token = default)
            where T: IEvent
        {
            var genericHanlerType = typeof(IEventHandler<T>);

            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => genericHanlerType.IsAssignableFrom(p))
                .Where(p => !p.IsAbstract);

            foreach (var item in types)
            {
                var instance = (IEventHandler<T>)Activator.CreateInstance(item);
                await instance.HandleAsync(domainEvent, token);
            }
        }

        public virtual void Dispatch<T>(T domainEvent)
            where T : IEvent
        {
            var genericHanlerType = typeof(IEventHandler<T>);

            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => genericHanlerType.IsAssignableFrom(p))
                .Where(p => !p.IsAbstract);

            foreach (var item in types)
            {
                var instance = (IEventHandler<T>)Activator.CreateInstance(item);
#pragma warning disable
                instance.HandleAsync(domainEvent);
#pragma warning enable
            }
        }
    }
}
