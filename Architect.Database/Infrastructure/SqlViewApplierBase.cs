using Architect.Common.Constants;
using Architect.Common.Infrastructure;

using Microsoft.EntityFrameworkCore;

namespace Architect.Database.Infrastructure
{
    public abstract class SqlViewApplierBase : ISqlViewApplier
    {
        protected abstract string ViewName { get; }
        protected abstract string QueryScript { get; }

        public virtual void Apply(ModelBuilder modelBuilder, DbContext context)
        {
            context.Database.ExecuteSqlCommand(new RawSqlString(
               @$"
                IF OBJECT_ID('[{DatabaseConsts.SCHEMA}].[{ViewName}]', 'V') IS NOT NULL
                    DROP VIEW [{DatabaseConsts.SCHEMA}].[{ViewName}];"));

            context.Database.ExecuteSqlCommand(new RawSqlString(
                @$"
                CREATE VIEW [{DatabaseConsts.SCHEMA}].[{ViewName}]
                AS
                {QueryScript};"));
        }
    }
}
