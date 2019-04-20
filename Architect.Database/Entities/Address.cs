using Architect.Database.Infrastructure;

using Microsoft.EntityFrameworkCore;

namespace Architect.Database.Entities
{
    public class Address : EntityBase
    {
        public Enums.Country Country { get; set; }
        public string City { get; set; }
        public int ZipCode { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }

        public Person Person { get; set; }
        public Company Company { get; set; }

        public override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Address>();

            entity.Property(x => x.City).IsRequired();
            entity.Property(x => x.Street).IsRequired();
            entity.Property(x => x.StreetNumber).IsRequired();

            entity.HasOne(x => x.Person).WithOne(x => x.Address)
                .HasForeignKey<Person>(x => x.AddressId).OnDelete(deleteBehavior);
            entity.HasOne(x => x.Company).WithOne(x => x.Address)
                .HasForeignKey<Company>(x => x.AddressId).OnDelete(deleteBehavior);

            base.OnModelCreating(modelBuilder);
        }
    }
}
