using System;

using Architect.Common.Infrastructure;

namespace Microsoft.EntityFrameworkCore
{
    public static class ModelBuilderExtensions
    {
        public static ModelBuilder CreateEntities(this ModelBuilder modelBuilder)
        {
            foreach (var instance in TypeExtensions.GetInstances<IEntityBase>())
            {
                instance.OnModelCreating(modelBuilder);
            }

            return modelBuilder;
        }
    }
}
