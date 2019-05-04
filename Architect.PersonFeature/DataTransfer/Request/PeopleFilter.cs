using Architect.Common.Infrastructure.DataTransfer.Request;

namespace Architect.PersonFeature.DataTransfer.Request
{
    public class PeopleFilter : PaginationFilter
    {
        public virtual string Filter { get; set; }
    }
}
