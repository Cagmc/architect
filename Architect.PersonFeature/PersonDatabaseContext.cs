using Microsoft.EntityFrameworkCore;

namespace Architect.PersonFeature
{
    public class PersonDatabaseContext : DbContext
    {
        public PersonDatabaseContext(DbContextOptions<PersonDatabaseContext> options) : base(options)
        {

        }

        public DbSet<Entities.PersonAggregate> People { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("per");

            base.OnModelCreating(modelBuilder);
        }
    }
}
