using System;
using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;

namespace Architect.Common.Infrastructure
{
    public abstract class EntityBase : IEntityBase
    {
        protected readonly DeleteBehavior deleteBehavior = DeleteBehavior.Restrict;

        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }

        public virtual void OnModelCreating(ModelBuilder modelBuilder)
        {
            ;
        }

        public virtual void SetAdded()
        {
            CreatedDate = DateTime.UtcNow;
        }

        public virtual void SetModified()
        {
            ModifiedDate = DateTime.UtcNow;
        }

        public virtual void SetDeleted()
        {
            ModifiedDate = DateTime.UtcNow;
            IsDeleted = true;
        }
    }
}
