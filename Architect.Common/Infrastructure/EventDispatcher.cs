using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Architect.Common.Infrastructure
{
    public class EventDispatcher : IEventDispatcher
    {
        protected readonly IServiceProvider provider;

        public EventDispatcher(IServiceProvider provider)
        {
            this.provider = provider;
        }

        public virtual async Task DispatchAsync<T>(T domainEvent, CancellationToken token = default)
            where T : IEvent
        {
            var types = typeof(IEventHandler<T>).GetConcreteTypes();

            foreach (var item in types)
            {
                var instance = CreateInstance<IEventHandler<T>>(item);
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
                var instance = CreateInstance<IEventHandler<T>>(item);
#pragma warning disable
                instance.HandleAsync(domainEvent);
#pragma warning enable
            }
        }

        protected virtual TResult CreateInstance<TResult>(Type type)
            where TResult : class
        {
            var constructor = type.GetConstructors()[0];

            if (constructor != null)
            {
                object[] args = constructor
                    .GetParameters()
                    .Select(o => o.ParameterType)
                    .Select(o => provider.GetService(o))
                    .ToArray();

                return Activator.CreateInstance(type, args) as TResult;
            }

            return null;
        }
    }
}
