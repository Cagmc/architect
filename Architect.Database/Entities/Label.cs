using System.Collections.Generic;

using Architect.Database.Infrastructure;

using Microsoft.EntityFrameworkCore;

namespace Architect.Database.Entities
{
    public class Label : EntityBase
    {
        public Profession Profession { get; set; }

        public IList<TranslatedLabel> TranslatedLabels { get; set; }

        public override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Label>();

            entity.HasOne(x => x.Profession).WithOne(x => x.Name)
                .HasForeignKey<Profession>(x => x.NameId).OnDelete(deleteBehavior);

            entity.HasMany(x => x.TranslatedLabels).WithOne(x => x.Label)
                .HasForeignKey(x => x.LabelId).OnDelete(deleteBehavior);

            entity.HasQueryFilter(x => !x.IsDeleted);

            base.OnModelCreating(modelBuilder);
        }
    }
}
