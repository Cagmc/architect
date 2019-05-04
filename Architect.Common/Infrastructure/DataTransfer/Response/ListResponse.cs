using System.Collections.Generic;
using System.Net;

namespace Architect.Common.Infrastructure.DataTransfer.Response
{
    public class ListResponse<T> : StatusResponse, IListResponse<T>
    {
        public ListResponse(IEnumerable<T> items, int totalCount, int totalPages, int? entityId = null) 
            : base(entityId)
        {
            Items = items ?? throw new System.ArgumentNullException(nameof(items));
            TotalCount = totalCount;
            TotalPages = totalPages;
        }

        public ListResponse(HttpStatusCode? statusCode, int? entityId = null)
            : base(statusCode, entityId)
        {
            Items = new List<T>();
            TotalCount = 0;
            TotalPages = 1;
        }

        public ListResponse(string errorMessage, HttpStatusCode? statusCode, int? entityId = null)
            : base(errorMessage, statusCode, entityId)
        {
            Items = new List<T>();
            TotalCount = 0;
            TotalPages = 1;
        }

        public virtual IEnumerable<T> Items { get; set; }
        public virtual int TotalCount { get; set; }
        public virtual int TotalPages { get; set; }
    }
}
