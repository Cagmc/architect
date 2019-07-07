using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

using Architect.UserFeature.DataTransfer.Request;
using Architect.UserFeature.DataTransfer.Response;
using Architect.UserFeature.Services;
using Architect.WebApp.Infrastructure;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Architect.WebApp.Controllers
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-2.2&tabs=visual-studio#migrating-to-aspnet-core-identity
    /// https://docs.microsoft.com/en-us/aspnet/core/migration/identity?view=aspnetcore-2.2
    /// </summary>
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class LoginController : ApiController
    {
        private readonly ILoginTransactionalService service;
        private readonly SignInManager<IdentityUser<int>> signInManager;

        public LoginController(ILoginTransactionalService service, SignInManager<IdentityUser<int>> signInManager)
        {
            this.service = service.ArgumentNullCheck(nameof(service));
            this.signInManager = signInManager;
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(SelfViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login(
            [Required][FromBody] LoginRequest request, CancellationToken token)
        {
            var result = await service.LoginAsync(request, token);

            return GenerateResponse(result);
        }

        [HttpPost("logout")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Logout(CancellationToken token)
        {
            var result = await service.LogoutAsync(token);

            return GenerateResponse(result);
        }

        [HttpGet("get-self")]
        [ProducesResponseType(typeof(SelfViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSelf(CancellationToken token)
        {
            var result = await service.GetSelfAsync(token);

            return GenerateResponse(result);
        }
    }
}
