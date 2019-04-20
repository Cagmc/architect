using System.Net;

namespace Architect.Common.Infrastructure.DataTransfer.Response
{
    public interface IStatusResponse
    {
        bool IsSuccess { get; }
        HttpStatusCode StatusCode { get; set; }
        string ErrorMessage { get; set; }
        int? EntityId { get; set; }
    }
}
