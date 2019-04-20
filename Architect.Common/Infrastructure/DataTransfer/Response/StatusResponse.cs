using System;
using System.Net;

namespace Architect.Common.Infrastructure.DataTransfer.Response
{
    public class StatusResponse
    {
        public StatusResponse(int? entityId)
        {
            EntityId = entityId;
            IsSuccess = true;
            StatusCode = HttpStatusCode.OK;
        }

        public StatusResponse(HttpStatusCode? statusCode, int? entityId)
        {
            EntityId = entityId;
            StatusCode = statusCode ?? HttpStatusCode.InternalServerError;
            IsSuccess = StatusCode == HttpStatusCode.OK;

            if (!IsSuccess)
            {
                ErrorMessage = "unspecified";
            }
        }

        public StatusResponse(string errorMessage, HttpStatusCode? statusCode, int? entityId)
        {
            if (errorMessage.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(errorMessage));
            }

            EntityId = entityId;
            ErrorMessage = errorMessage;
            StatusCode = statusCode ?? HttpStatusCode.InternalServerError;
            IsSuccess = false;
        }

        public virtual bool IsSuccess { get; }
        public virtual HttpStatusCode StatusCode { get; set; }
        public virtual string ErrorMessage { get; set; }
        public virtual int? EntityId { get; set; }
    }
}
