using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

using Architect.UserFeature.DataTransfer.Request;
using Architect.UserFeature.Services;
using Architect.WebApp.Infrastructure;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Architect.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class RegistrationController : ApiController
    {
        private readonly IRegistrationTransactionalService service;

        public RegistrationController(IRegistrationTransactionalService service)
        {
            this.service = service.ArgumentNullCheck(nameof(service));
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Register(
            [Required][FromBody] RegistrationRequest request, CancellationToken token)
        {
            var result = await service.RegistrationAsync(request, token);

            return GenerateResponse(result);
        }
    }
}
