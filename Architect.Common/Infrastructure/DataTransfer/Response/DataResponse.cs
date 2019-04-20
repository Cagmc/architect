using System.Net;

namespace Architect.Common.Infrastructure.DataTransfer.Response
{
    public class DataResponse<T> : StatusResponse
    {
        public DataResponse(T data, int? entityId) : base(entityId)
        {
            Data = data;
        }

        public DataResponse(T data, HttpStatusCode? statusCode, int? entityId) : base(statusCode, entityId)
        {
            Data = data;
        }

        public DataResponse(string errorMessage, HttpStatusCode? statusCode, int? entityId) 
            : base(errorMessage, statusCode, entityId)
        {
        }

        public virtual T Data { get; set; }
    }
}
