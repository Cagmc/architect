using System.Collections.Generic;

using Architect.Database.Infrastructure;

using Microsoft.EntityFrameworkCore;

namespace Architect.Database.Entities
{
    public class Profession : EntityBase
    {
        public int NameId { get; set; }
        public Label Name { get; set; }

        public IList<Employment> Employments { get; set; }

        public override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Profession>();

            entity.HasOne(x => x.Name).WithOne(x => x.Profession)
                .HasForeignKey<Profession>(x => x.NameId).OnDelete(deleteBehavior);

            entity.HasMany(x => x.Employments).WithOne(x => x.Job)
                .HasForeignKey(x => x.JobId).OnDelete(deleteBehavior);

            entity.HasQueryFilter(x => !x.IsDeleted);

            base.OnModelCreating(modelBuilder);
        }
    }
}
