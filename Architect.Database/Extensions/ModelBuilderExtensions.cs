using System;
using System.Linq;

using Architect.Common.Infrastructure;

namespace Microsoft.EntityFrameworkCore
{
    public static class ModelBuilderExtensions
    {
        public static ModelBuilder CreateEntities(this ModelBuilder modelBuilder)
        {
            var type = typeof(IEntityBase);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p))
                .Where(p => !p.IsAbstract);

            foreach (var item in types)
            {
                var instance = (IEntityBase)Activator.CreateInstance(item);
                instance.OnModelCreating(modelBuilder);
            }

            return modelBuilder;
        }

        public static ModelBuilder CreateViews(this ModelBuilder modelBuilder, DbContext context)
        {
            context.Database.ExecuteSqlCommand(new RawSqlString(
               @"
                IF OBJECT_ID('[arc].[PersonView]', 'V') IS NOT NULL
                    DROP VIEW [arc].[PersonView];"));

            context.Database.ExecuteSqlCommand(new RawSqlString(
                @"
                CREATE VIEW [arc].[PersonView]
                AS
                SELECT [p].[Id] AS [PersonId]
                   ,CONCAT([n].[FirstName], ' ', [n].[LastName]) AS [Name]
                   ,CONCAT([a].[Country], ',', [a].[City]) AS [Address]
                FROM [arc].[People] AS [p]
                JOIN [arc].[Names] AS [n]
                   ON [p].[NameId] = [n].[Id]
                JOIN [arc].[Addresses] AS [a]
                   ON [p].[AddressId] = [a].[Id];"));

            return modelBuilder;
        }
    }
}
