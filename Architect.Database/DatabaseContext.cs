using System.Threading;
using System.Threading.Tasks;

using Architect.Common.Infrastructure;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Architect.Database
{
    public class DatabaseContext : IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public DbQuery<QueryTypes.PersonSqlQuery> PersonSqlQueries { get; set; }
        public DbQuery<QueryTypes.PersonViewQuery> PersonViews { get; set; }

        public DbSet<Entities.Address> Addresses { get; set; }
        public DbSet<Entities.AddressAggregate> AddressAggregates { get; set; }
        public DbSet<Entities.BackgroundJob> BackgroundJobs { get; set; }
        public DbSet<Entities.Company> Companies { get; set; }
        public DbSet<Entities.Employment> Employments { get; set; }
        public DbSet<Entities.Label> Labels { get; set; }
        public DbSet<Entities.Language> Languages { get; set; }
        public DbSet<Entities.Name> Names { get; set; }
        public DbSet<Entities.NameAggregate> NameAggregates { get; set; }
        public DbSet<Entities.Person> People { get; set; }
        public DbSet<Entities.PersonAggregate> PersonAggregates { get; set; }
        public DbSet<Entities.Profession> Professions { get; set; }
        public DbSet<Entities.TranslatedLabel> TranslatedLabels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("arc");

            modelBuilder.CreateViews(this);
            modelBuilder.CreateEntities();

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            OnBeforeSaveCahnges();

            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaveCahnges();

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            OnBeforeSaveCahnges();

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            OnBeforeSaveCahnges();

            return base.SaveChangesAsync(cancellationToken);
        }

        private void OnBeforeSaveCahnges()
        {
            var changed = ChangeTracker.Entries();

            foreach (var entityEntry in changed)
            {
                SetMetadata(entityEntry);
            }
        }

        private void SetMetadata(EntityEntry entityEntry)
        {
            if (entityEntry.Entity is EntityBase entity)
            {
                switch (entityEntry.State)
                {
                    case EntityState.Deleted:
                        entityEntry.State = EntityState.Modified;
                        entity.SetDeleted();
                        break;
                    case EntityState.Modified:
                        entity.SetModified();
                        break;
                    case EntityState.Added:
                        entity.SetAdded();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
