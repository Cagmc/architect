using Architect.Common.Infrastructure.DataTransfer.Request;

namespace Architect.PersonFeature.DataTransfer.Request
{
    public class AddressFilter : PaginationFilter
    {
        public virtual int? Id { get; set; }
        public virtual string Filter { get; set; }
    }
}
