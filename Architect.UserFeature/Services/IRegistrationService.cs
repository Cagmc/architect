using System.Threading;
using System.Threading.Tasks;

using Architect.Common.Infrastructure;
using Architect.Common.Infrastructure.DataTransfer.Response;
using Architect.UserFeature.DataTransfer.Request;

namespace Architect.UserFeature.Services
{
    public interface IRegistrationService : IService
    {
        Task<IStatusResponse> RegistrationAsync(RegistrationRequest request, CancellationToken token = default);
    }
}
