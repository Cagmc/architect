using System.Threading;
using System.Threading.Tasks;

using Architect.Common.Constants;
using Architect.Common.Infrastructure;

using Microsoft.EntityFrameworkCore;

namespace Architect.Database.Infrastructure
{
    public abstract class SqlViewApplierBase : ISqlViewApplier
    {
        protected abstract string ViewName { get; }
        protected abstract string QueryScript { get; }

        public virtual void Apply(DbContext context)
        {
            context.Database.ExecuteSqlCommand(Drop());
            context.Database.ExecuteSqlCommand(Create());
        }

        public async Task ApplyAsync(DbContext context, CancellationToken token = default)
        {
            await context.Database.ExecuteSqlCommandAsync(Drop(), token).ConfigureAwaitFalse();
            await context.Database.ExecuteSqlCommandAsync(Create(), token).ConfigureAwaitFalse();
        }

        private RawSqlString Drop()
        {
            var script = new RawSqlString(
               @$"
                IF OBJECT_ID('[{DatabaseConsts.SCHEMA}].[{ViewName}]', 'V') IS NOT NULL
                    DROP VIEW [{DatabaseConsts.SCHEMA}].[{ViewName}];");

            return script;
        }

        private RawSqlString Create()
        {
            var script = new RawSqlString(
                @$"
                CREATE VIEW [{DatabaseConsts.SCHEMA}].[{ViewName}]
                AS
                {QueryScript};");

            return script;
        }
    }
}
