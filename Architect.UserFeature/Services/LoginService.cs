using System;
using System.Threading;
using System.Threading.Tasks;

using Architect.Common.Infrastructure;
using Architect.Common.Infrastructure.DataTransfer.Response;
using Architect.Database;
using Architect.UserFeature.DataTransfer.Request;
using Architect.UserFeature.DataTransfer.Response;

namespace Architect.UserFeature.Services
{
    public class LoginService : ServiceBase<DatabaseContext>, ILoginService
    {
        public LoginService(DatabaseContext context) : base(context)
        {

        }

        public Task<IDataResponse<SelfViewModel>> GetSelfAsync(CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public Task<IDataResponse<SelfViewModel>> LoginAsync(LoginRequest model, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public Task<IStatusResponse> LogoutAsync(CancellationToken token = default)
        {
            throw new NotImplementedException();
        }
    }
}
