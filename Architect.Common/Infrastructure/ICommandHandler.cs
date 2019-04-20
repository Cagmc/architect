using System.Threading;
using System.Threading.Tasks;

namespace Architect.Common.Infrastructure
{
    public interface ICommandHandler<T> where T: ICommand
    {
        Task HandleAsync(T command, CancellationToken token = default);
    }
}
