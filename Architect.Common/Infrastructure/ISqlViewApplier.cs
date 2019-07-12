using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace Architect.Common.Infrastructure
{
    public interface ISqlViewApplier
    {
        void Apply(DbContext context);
        Task ApplyAsync(DbContext context, CancellationToken token = default);
    }
}
