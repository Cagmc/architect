namespace Architect.Common.Infrastructure.DataTransfer.Request
{
    public class PaginationFilter
    {
        public virtual int? Page { get; set; }
        public virtual int? PageSize { get; set; }
        public virtual string OrderByColumn { get; set; }
        public Enums.OrderDirection OrderByDirection { get; set; }
    }
}
