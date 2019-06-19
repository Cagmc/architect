using System.Threading;
using System.Threading.Tasks;

using Architect.Common.Infrastructure;
using Architect.Common.Infrastructure.DataTransfer.Response;
using Architect.UserFeature.DataTransfer.Request;
using Architect.UserFeature.DataTransfer.Response;

namespace Architect.UserFeature.Services
{
    public interface ILoginService : IService
    {
        Task<IDataResponse<SelfViewModel>> LoginAsync(
            LoginRequest model, CancellationToken token = default);

        Task<IStatusResponse> LogoutAsync(CancellationToken token = default);

        Task<IDataResponse<SelfViewModel>> GetSelfAsync(CancellationToken token = default);
    }
}
