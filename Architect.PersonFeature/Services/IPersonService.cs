using System.Threading;
using System.Threading.Tasks;

using Architect.Common.Infrastructure;
using Architect.Common.Infrastructure.DataTransfer.Response;
using Architect.PersonFeature.DataTransfer.Request;
using Architect.PersonFeature.DataTransfer.Response;

namespace Architect.PersonFeature.Services
{
    public interface IPersonService : IDomainService<PersonViewModel, CreatePersonRequest, UpdatePersonRequest,DeletePersonRequest>
    {
        Task<IStatusResponse> ChangeAddressAsync(
            ChangeAddressRequest model, CancellationToken token = default);

        Task<IStatusResponse> ChangeNameAsync(
            ChangeNameRequest model, CancellationToken token = default);
    }
}
