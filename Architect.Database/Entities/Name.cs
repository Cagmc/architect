using Architect.Database.Infrastructure;

using Microsoft.EntityFrameworkCore;

namespace Architect.Database.Entities
{
    public class Name : EntityBase
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public string FullName { get { return $"{FirstName} {LastName}"; } }
        public Person Person { get; set; }

        public override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Name>();

            entity.Property(x => x.FirstName).IsRequired();
            entity.Property(x => x.LastName).IsRequired();
            entity.Ignore(x => x.FullName);

            entity.HasOne(x => x.Person).WithOne(x => x.Name)
                .HasForeignKey<Person>(x => x.NameId).OnDelete(deleteBehavior);

            entity.HasQueryFilter(x => !x.IsDeleted);

            base.OnModelCreating(modelBuilder);
        }
    }
}
