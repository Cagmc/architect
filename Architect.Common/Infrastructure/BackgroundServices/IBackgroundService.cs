using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Hosting;

namespace Architect.Common.Infrastructure.BackgroundServices
{
    public interface IBackgroundService : IHostedService
    {
        Task DoWorkAsync(CancellationToken token = default);
    }
}
