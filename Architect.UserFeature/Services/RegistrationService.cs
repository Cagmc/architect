using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

using Architect.Common.Infrastructure;
using Architect.Common.Infrastructure.DataTransfer.Response;
using Architect.Database;
using Architect.UserFeature.DataTransfer.Request;

using Microsoft.AspNetCore.Identity;

namespace Architect.UserFeature.Services
{
    public class RegistrationService : ServiceBase<DatabaseContext>, IRegistrationService
    {
        private readonly SignInManager<IdentityUser<int>> signInManager;

        public RegistrationService(
            SignInManager<IdentityUser<int>> signInManager, 
            DatabaseContext context) : base(context)
        {
            this.signInManager = signInManager.ArgumentNullCheck(nameof(signInManager));
        }

        public async Task<IStatusResponse> RegistrationAsync(
            RegistrationRequest request, CancellationToken token = default)
        {
            request.ArgumentNullCheck(nameof(request));

            var newUser = new IdentityUser<int>(request.Email) { Email = request.Email };
            var createResult = await signInManager.UserManager.CreateAsync(newUser, request.Password);

            if (createResult.Succeeded)
            {
                return new StatusResponse(null);
            }
            else
            {
                var errors = string.Join(";", createResult.Errors.Select(
                    x => $"Code: {x.Code}, Description: {x.Description}"));

                return new StatusResponse(errors, HttpStatusCode.Conflict, null);
            }
        }
    }
}
