using System.Collections.Generic;
using System.Net;

namespace Architect.Common.Infrastructure.DataTransfer.Response
{
    public class ListResponse<T> : StatusResponse
    {
        public ListResponse(IEnumerable<T> items, int totalCount, int? entityId) : base(entityId)
        {
            Items = items ?? throw new System.ArgumentNullException(nameof(items));
            TotalCount = totalCount;
        }

        public ListResponse(HttpStatusCode? statusCode, int? entityId) 
            : base(statusCode, entityId)
        {
        }

        public ListResponse(string errorMessage, HttpStatusCode? statusCode, int? entityId) 
            : base(errorMessage, statusCode, entityId)
        {
            Items = new List<T>();
            TotalCount = 0;
        }

        public virtual IEnumerable<T> Items { get; set; }
        public virtual int TotalCount { get; set; }
    }
}
