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
            var types = typeof(ISqlViewApplier).GetConcreteTypes();

            foreach (var item in types)
            {
                var instance = (ISqlViewApplier)Activator.CreateInstance(item);
                instance.Apply(context);
            }

            return context;
        }

        public static async Task<DbContext> CreateViewsAsync(this DbContext context, CancellationToken token = default)
        {
            var types = typeof(ISqlViewApplier).GetConcreteTypes();

            foreach (var item in types)
            {
                var instance = (ISqlViewApplier)Activator.CreateInstance(item);
                await instance.ApplyAsync(context, token).ConfigureAwait(false);
            }

            return context;
        }
    }
}
