using Microsoft.EntityFrameworkCore;

namespace Architect.Common.Infrastructure
{
    public interface ISqlViewApplier
    {
        void Apply(ModelBuilder modelBuilder, DbContext context);
    }
}
