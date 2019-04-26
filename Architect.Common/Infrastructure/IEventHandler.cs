using System.Threading;
using System.Threading.Tasks;

namespace Architect.Common.Infrastructure
{
    public interface IEventHandler<T> where T: IEvent
    {
        Task HandleAsync(T command, CancellationToken token = default);
    }
}
