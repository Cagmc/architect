using Architect.Common.Constants;
using Architect.Database.Infrastructure;

namespace Architect.PersonFeature.Queries
{
    public class PersonSqlViewApplier : SqlViewApplierBase
    {
        protected override string ViewName => DatabaseConsts.PERSONVIEW;

        protected override string QueryScript =>
            @"  SELECT [p].[Id] AS [PersonId]
                   ,CONCAT([n].[FirstName], ' ', [n].[LastName]) AS [Name]
                   ,CONCAT([a].[Country], ',', [a].[City]) AS [Address]
                FROM [arc].[People] AS [p]
                JOIN [arc].[Names] AS [n]
                   ON [p].[NameId] = [n].[Id]
                JOIN [arc].[Addresses] AS [a]
                   ON [p].[AddressId] = [a].[Id]";
    }
}
