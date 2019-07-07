using Microsoft.EntityFrameworkCore;

namespace Architect.Common.Infrastructure
{
    public interface IEntityBase
    {
        void OnModelCreating(ModelBuilder modelBuilder);
    }
}
