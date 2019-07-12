using System;

using Architect.Common.Infrastructure;

namespace Microsoft.EntityFrameworkCore
{
    public static class ModelBuilderExtensions
    {
        public static ModelBuilder CreateEntities(this ModelBuilder modelBuilder)
        {
            var types = typeof(IEntityBase).GetConcreteTypes();

            foreach (var item in types)
            {
                var instance = (IEntityBase)Activator.CreateInstance(item);
                instance.OnModelCreating(modelBuilder);
            }

            return modelBuilder;
        }
    }
}
