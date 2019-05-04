using System;

using Architect.Common.Infrastructure;

using Microsoft.EntityFrameworkCore;

namespace Architect.Database.Entities
{
    public class Employment : EntityBase
    {
        public bool IsIndefinitePeriod { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public int EmployeeId { get; set; }
        public Person Employee { get; set; }

        public int EmployerId { get; set; }
        public Company Employer { get; set; }

        public int JobId { get; set; }
        public Profession Job { get; set; }

        public override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Employment>();

            entity.HasOne(x => x.Employee).WithMany(x => x.Employments)
                .HasForeignKey(x => x.EmployeeId).OnDelete(deleteBehavior);
            entity.HasOne(x => x.Employer).WithMany(x => x.Employments)
                .HasForeignKey(x => x.EmployerId).OnDelete(deleteBehavior);
            entity.HasOne(x => x.Job).WithMany(x => x.Employments)
                .HasForeignKey(x => x.JobId).OnDelete(deleteBehavior);

            entity.HasQueryFilter(x => !x.IsDeleted);

            base.OnModelCreating(modelBuilder);
        }
    }
}
