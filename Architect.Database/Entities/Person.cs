using System;
using System.Collections.Generic;

using Architect.Database.Infrastructure;

using Microsoft.EntityFrameworkCore;

namespace Architect.Database.Entities
{
    public class Person : EntityBase
    {
        public int Height { get; set; }
        public int Weight { get; set; }
        public Enums.Color HairColor { get; set; }
        public Enums.Color EyeColor { get; set; }
        public DateTime BirthDate { get; set; }

        public int NameId { get; set; }
        public Name Name { get; set; }

        public int? AddressId { get; set; }
        public Address Address { get; set; }

        public IList<Company> CompaniesLead { get; set; }
        public IList<Employment> Employments { get; set; }

        public override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Person>();

            entity.HasOne(x => x.Name).WithOne(x => x.Person)
                .HasForeignKey<Person>(x => x.NameId).OnDelete(deleteBehavior);
            entity.HasOne(x => x.Address).WithOne(x => x.Person)
                .HasForeignKey<Person>(x => x.AddressId).OnDelete(deleteBehavior);

            entity.HasMany(x => x.CompaniesLead).WithOne(x => x.Chairman)
                .HasForeignKey(x => x.ChairmanId).OnDelete(deleteBehavior);
            entity.HasMany(x => x.Employments).WithOne(x => x.Employee)
                .HasForeignKey(x => x.EmployeeId).OnDelete(deleteBehavior);

            base.OnModelCreating(modelBuilder);
        }
    }
}
