using System.Collections.Generic;

namespace Architect.Common.Infrastructure.DataTransfer.Response
{
    public interface IListResponse<T> : IStatusResponse
    {
        IEnumerable<T> Items { get; set; }
        int TotalCount { get; set; }
    }
}
