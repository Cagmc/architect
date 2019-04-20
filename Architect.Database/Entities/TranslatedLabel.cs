using Architect.Database.Infrastructure;

using Microsoft.EntityFrameworkCore;

namespace Architect.Database.Entities
{
    public class TranslatedLabel : EntityBase
    {
        public string Text { get; set; }

        public int LabelId { get; set; }
        public Label Label { get; set; }

        public int LanguageId { get; set; }
        public Language Language { get; set; }

        public override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<TranslatedLabel>();

            entity.Property(x => x.Text).IsRequired();

            entity.HasOne(x => x.Label).WithMany(x => x.TranslatedLabels)
                .HasForeignKey(x => x.LabelId).OnDelete(deleteBehavior);
            entity.HasOne(x => x.Language).WithMany(x => x.TranslatedLabels)
                .HasForeignKey(x => x.LanguageId).OnDelete(deleteBehavior);

            entity.HasQueryFilter(x => !x.IsDeleted);

            base.OnModelCreating(modelBuilder);
        }
    }
}
