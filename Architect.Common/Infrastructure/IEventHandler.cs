using System.Threading;
using System.Threading.Tasks;

namespace Architect.Common.Infrastructure
{
    public interface IEventHandler<T> where T: IEvent
    {
        Task HandleAsync(T data, CancellationToken token = default);
    }
}
