using System.Collections.Generic;

using Architect.Common.Infrastructure;

using Microsoft.EntityFrameworkCore;

namespace Architect.Database.Entities
{
    public class Language : EntityBase
    {
        public string EnglishName { get; set; }
        public string NativeName { get; set; }

        public IList<TranslatedLabel> TranslatedLabels { get; set; }

        public override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Language>();

            entity.Property(x => x.EnglishName).IsRequired();
            entity.Property(x => x.NativeName).IsRequired();

            entity.HasMany(x => x.TranslatedLabels).WithOne(x => x.Language)
                .HasForeignKey(x => x.LanguageId).OnDelete(deleteBehavior);

            entity.HasQueryFilter(x => !x.IsDeleted);

            base.OnModelCreating(modelBuilder);
        }
    }
}
