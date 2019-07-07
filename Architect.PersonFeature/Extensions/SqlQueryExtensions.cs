using System.Linq;

using Microsoft.EntityFrameworkCore;

namespace Architect.Database.QueryTypes
{
    public static class SqlQueryExtensions
    {
        public static IQueryable<PersonSqlQuery> PersonSqlQuery(this DbQuery<PersonSqlQuery> query)
        {
            var sqlQuery = query.FromSql(new RawSqlString(
                @"SELECT [p].[Id] AS [PersonId]
                   , CONCAT([n].[FirstName], ' ', [n].[LastName]) AS[Name]
                   ,CONCAT([a].[Country], ',', [a].[City]) AS[Address]
                FROM [arc].[People]
                        AS[p]
                JOIN [arc].[Names]
                        AS[n]
                ON [p].[NameId] = [n].[Id]
                        JOIN[arc].[Addresses]
                        AS[a]
                ON [p].[AddressId] = [a].[Id];"));

            return sqlQuery;
        }
    }
}
