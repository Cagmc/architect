using System;
using System.Collections.Generic;

using Architect.Common.Infrastructure;

using Microsoft.EntityFrameworkCore;

namespace Architect.Database.Entities
{
    public class Company : EntityBase
    {
        public string Name { get; set; }
        public DateTime FoundationDate { get; set; }

        public int ChairmanId { get; set; }
        public Person Chairman { get; set; }

        public int AddressId { get; set; }
        public Address Address { get; set; }

        public IList<Employment> Employments { get; set; }

        public override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Company>();

            entity.Property(x => x.Name).IsRequired();

            entity.HasOne(x => x.Chairman).WithMany(x => x.CompaniesLead)
                .HasForeignKey(x => x.ChairmanId).OnDelete(deleteBehavior);
            entity.HasOne(x => x.Address).WithOne(x => x.Company).
                HasForeignKey<Company>(x => x.AddressId).OnDelete(deleteBehavior);

            entity.HasMany(x => x.Employments).WithOne(x => x.Employer)
                .HasForeignKey(x => x.EmployerId).OnDelete(deleteBehavior);

            entity.HasQueryFilter(x => !x.IsDeleted);

            base.OnModelCreating(modelBuilder);
        }
    }
}
