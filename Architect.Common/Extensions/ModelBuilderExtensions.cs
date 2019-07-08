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

        public static ModelBuilder CreateViews(this ModelBuilder modelBuilder, DbContext context)
        {
            var types = typeof(ISqlViewApplier).GetConcreteTypes();

            foreach (var item in types)
            {
                var instance = (ISqlViewApplier)Activator.CreateInstance(item);
                instance.Apply(modelBuilder, context);
            }

            return modelBuilder;
        }
    }
}
