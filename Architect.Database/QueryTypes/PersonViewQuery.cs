using Architect.Common.Constants;
using Architect.Common.Infrastructure;

using Microsoft.EntityFrameworkCore;

namespace Architect.Database.QueryTypes
{
    public class PersonViewQuery : IEntityBase
    {
        public int PersonId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Query<PersonViewQuery>().ToView(DatabaseConsts.PERSONVIEW, DatabaseConsts.SCHEMA);
        }
    }
}
