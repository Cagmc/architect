using Architect.Common.Infrastructure;

using Microsoft.EntityFrameworkCore;

namespace Architect.Database.QueryTypes
{
    public class PersonSqlQuery : IEntityBase
    {
        public int PersonId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public void OnModelCreating(ModelBuilder modelBuilder)
        {
            return;
        }
    }
}
