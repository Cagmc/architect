using Architect.Database.Entities;
using Architect.PersonFeature.DataTransfer.Response;

namespace System.Linq
{
    internal static class QueryExtensions
    {
        internal static IQueryable<PersonOverviewViewModel> SelectPersonOverviewViewModel(
            this IQueryable<Person> query)
        {
            var sQuery = query
            .Select(x => new PersonOverviewViewModel(x.Id)
            {
                Name = x.Name.FullName
            });

            return sQuery;
        }
    }
}
