using System.Threading;
using System.Threading.Tasks;

namespace Architect.WebApp.BackgroundTasks
{
    internal interface IScopedProcessingService
    {
        Task DoWorkAsync(CancellationToken token = default);
    }
}
