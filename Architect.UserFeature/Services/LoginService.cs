using System;
using System.Threading;
using System.Threading.Tasks;

using Architect.Common.Constants;
using Architect.Common.Infrastructure;
using Architect.Common.Infrastructure.DataTransfer.Response;
using Architect.Database;
using Architect.UserFeature.DataTransfer.Request;
using Architect.UserFeature.DataTransfer.Response;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Architect.UserFeature.Services
{
    public class LoginService : ServiceBase<DatabaseContext>, ILoginService
    {
        private readonly SignInManager<IdentityUser<int>> signInManager;
        private readonly RoleManager<IdentityRole<int>> roleManager;
        private readonly IHttpContextAccessor httpContext;

        public LoginService(DatabaseContext context,
            SignInManager<IdentityUser<int>> signInManager,
            RoleManager<IdentityRole<int>> roleManager, 
            IHttpContextAccessor httpContext) : base(context)
        {
            this.signInManager = signInManager.ArgumentNullCheck(nameof(signInManager));
            this.roleManager = roleManager.ArgumentNullCheck(nameof(roleManager));
            this.httpContext = httpContext.ArgumentNullCheck(nameof(roleManager));
        }

        public async Task<IDataResponse<SelfViewModel>> GetSelfAsync(CancellationToken token = default)
        {
            var principal = httpContext.HttpContext.User;

            if (principal == null || principal.Identity == null || principal.Identity.Name.IsNullOrEmpty())
            {
                return new DataResponse<SelfViewModel>(
                    "not_logged_in", System.Net.HttpStatusCode.Conflict, null);
            }

            var user = await signInManager.UserManager.FindByEmailAsync(principal.Identity.Name)
                .ConfigureAwaitFalse();

            var result = new SelfViewModel
            {
                Id = user.Id,
                Email = user.Email
            };

            return new DataResponse<SelfViewModel>(result, null);
        }

        public async Task<IDataResponse<SelfViewModel>> LoginAsync(
            LoginRequest model, CancellationToken token = default)
        {
            model.ArgumentNullCheck(nameof(model));

            var signInResult = await signInManager.PasswordSignInAsync(
                model.Email, model.Password, model.IsPersistent, false)
                .ConfigureAwaitFalse();

            if (signInResult.Succeeded)
            {
                var identityUser = await signInManager.UserManager.FindByEmailAsync(model.Email)
                    .ConfigureAwaitFalse();

                if (!await signInManager.UserManager.IsInRoleAsync(identityUser, Roles.Administrators))
                {
                    if (!await roleManager.RoleExistsAsync(Roles.Administrators).ConfigureAwaitFalse())
                    {
                        await roleManager.CreateAsync(new IdentityRole<int>(Roles.Administrators))
                            .ConfigureAwaitFalse();
                    }

                    await signInManager.UserManager.AddToRoleAsync(identityUser, Roles.Administrators)
                        .ConfigureAwaitFalse();
                }

                var result = new SelfViewModel { Id = identityUser.Id, Email = identityUser.Email };

                return new DataResponse<SelfViewModel>(result, null);
            }
            else
            {
                return new DataResponse<SelfViewModel>(
                    $"{model.Email}", System.Net.HttpStatusCode.Unauthorized, null);
            }
        }

        public async Task<IStatusResponse> LogoutAsync(CancellationToken token = default)
        {
            await signInManager.SignOutAsync().ConfigureAwaitFalse();

            return new StatusResponse(System.Net.HttpStatusCode.OK, null);
        }
    }
}
