using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

using Architect.Common.Constants;
using Architect.UserFeature.DataTransfer.Request;
using Architect.WebApp.Infrastructure;

using Microsoft.AspNetCore.Authorization;
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
        private readonly SignInManager<IdentityUser<int>> signInManager;
        private readonly IHttpContextAccessor httpContext;
        private readonly RoleManager<IdentityRole<int>> roleManager;

        public LoginController(SignInManager<IdentityUser<int>> signInManager, IHttpContextAccessor httpContext, RoleManager<IdentityRole<int>> roleManager)
        {
            this.signInManager = signInManager;
            this.httpContext = httpContext;
            this.roleManager = roleManager;
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<IdentityError>), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Register(
            [Required][FromBody] RegistrationRequest request, CancellationToken token)
        {
            var newUser = new IdentityUser<int>(request.Email) { Email = request.Email };
            var createResult = await signInManager.UserManager.CreateAsync(newUser, request.Password);

            if (createResult.Succeeded)
            {
                return Ok();
            }
            else
            {
                return Conflict(createResult.Errors);
            }
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login(
            [Required][FromBody] LoginRequest request, CancellationToken token)
        {
            var signInResult = await signInManager.PasswordSignInAsync(
                request.Email, request.Password, request.IsPersistent, false);

            if (signInResult.Succeeded)
            {
                var identityUser = await signInManager.UserManager.FindByEmailAsync(request.Email);

                if (!await signInManager.UserManager.IsInRoleAsync(identityUser, Roles.Administrators))
                {
                    if(!await roleManager.RoleExistsAsync(Roles.Administrators))
                    {
                        await roleManager.CreateAsync(new IdentityRole<int>(Roles.Administrators));
                    }

                    await signInManager.UserManager.AddToRoleAsync(identityUser, Roles.Administrators);
                }

                return Ok();
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPost("logout")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Logout(CancellationToken token)
        {
            await signInManager.SignOutAsync();

            return Ok();
        }

        [HttpGet("is-signed-in")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public IActionResult IsSignedIn()
        {
            var isSignedIn = signInManager.IsSignedIn(User);

            return Ok(isSignedIn);
        }

        [HttpGet("authorized")]
        [Authorize]
        [ProducesResponseType(200)]
        public IActionResult Authorized()
        {
            return Ok();
        }

        [HttpGet("policy-authorized")]
        [Authorize(Policies.Administration)]
        [ProducesResponseType(200)]
        public IActionResult AdminAuthorized()
        {
            var user = User;
            var user2 = httpContext.HttpContext.User;

            return Ok();
        }
    }
}
