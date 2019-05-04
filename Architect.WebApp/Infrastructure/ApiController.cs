using System;
using System.Net;

using Architect.Common.Infrastructure.DataTransfer.Response;

using Microsoft.AspNetCore.Mvc;

namespace Architect.WebApp.Infrastructure
{
    public abstract class ApiController : ControllerBase
    {
        protected virtual IActionResult GenerateResponse<TItems>(IListResponse<TItems> response)
        {
            return Ok(response);
        }

        protected virtual IActionResult GenerateResponse(IStatusResponse response, bool isCreated = false)
        {
            if (response.IsSuccess)
            {
                if (isCreated)
                {
                    return CreatedAtAction("Get", new { id = response.EntityId });
                }
                else
                {
                    return Ok(response.EntityId);
                }
            }
            else
            {
                return GenerateProblemResult(response.StatusCode, response.ErrorMessage);
            }
        }

        protected virtual IActionResult GenerateResponse<T>(IDataResponse<T> response)
        {
            if (response.IsSuccess)
            {
                return Ok(response.Data);
            }
            else
            {
                return GenerateProblemResult(response.StatusCode, response.ErrorMessage);
            }
        }

        protected virtual IActionResult GenerateProblemResult(HttpStatusCode statusCode, string message)
        {
            var details = new ProblemDetails
            {
                Detail = message,
                Status = (int)statusCode,
                Title = Enum.GetName(typeof(HttpStatusCode), statusCode)
            };

            return StatusCode(details.Status.Value, details);
        }
    }
}
