using System;
using System.Threading;
using System.Threading.Tasks;

using Architect.Common.Infrastructure;

namespace Microsoft.EntityFrameworkCore
{
    public static class DbContextExtensions
    {
        public static DbContext CreateViews(this DbContext context)
        {
            foreach (var instance in TypeExtensions.GetInstances<ISqlViewApplier>())
            {
                instance.Apply(context);
            }

            return context;
        }

        public static async Task<DbContext> CreateViewsAsync(this DbContext context, CancellationToken token = default)
        {
            foreach (var instance in TypeExtensions.GetInstances<ISqlViewApplier>())
            {
                await instance.ApplyAsync(context, token).ConfigureAwaitFalse();
            }

            return context;
        }
    }
}
