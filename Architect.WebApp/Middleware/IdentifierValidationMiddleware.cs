using System.Linq;
using System.Net;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

namespace Architect.WebApp.Middleware
{
    public class IdentifierValidationMiddleware
    {
        private readonly RequestDelegate _next;

        public IdentifierValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var id = context.Request.Query.Where(x => x.Key == "id")
                .SingleOrDefault();

            // http://anthonygiretti.com/2018/09/04/asp-net-core-2-1-middlewares-part1-building-a-custom-middleware/
            if (id.Value == "0")
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else if (context.Request.Path.Value.EndsWith("/0"))
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else
            {
                await _next.Invoke(context);
            }
        }
    }
}
