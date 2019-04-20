﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Architect.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Entities.Address> Addresses { get; set; }
        public DbSet<Entities.Company> Companies { get; set; }
        public DbSet<Entities.Employment> Employments { get; set; }
        public DbSet<Entities.Label> Labels { get; set; }
        public DbSet<Entities.Language> Languages { get; set; }
        public DbSet<Entities.Name> Names { get; set; }
        public DbSet<Entities.Person> People { get; set; }
        public DbSet<Entities.Profession> Professions { get; set; }
        public DbSet<Entities.TranslatedLabel> TranslatedLabels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("arc");

            OnModelCreatingEntities(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void OnModelCreatingEntities(ModelBuilder modelBuilder)
        {
            var type = typeof(Infrastructure.EntityBase);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p))
                .Where(p => !p.IsAbstract);

            foreach (var item in types)
            {
                var instance = (Infrastructure.EntityBase)Activator.CreateInstance(item);
                instance.OnModelCreating(modelBuilder);
            }
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
            if (entityEntry.Entity is Infrastructure.EntityBase entity)
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
                        entity.SetDeleted();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}