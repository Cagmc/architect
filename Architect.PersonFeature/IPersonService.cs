using System.Threading;
using System.Threading.Tasks;

using Architect.Common.Infrastructure.DataTransfer.Response;
using Architect.PersonFeature.DataTransfer.Request;
using Architect.PersonFeature.DataTransfer.Response;

namespace Architect.PersonFeature
{
    public interface IPersonService
    {
        Task<IDataResponse<PersonViewModel>> GetAsync(int id, CancellationToken token = default);
        Task<IStatusResponse> CreateAsync(CreatePersonRequest model, CancellationToken token = default);
        Task<IStatusResponse> UpdateAsync(UpdatePersonRequest model, CancellationToken token = default);
        Task<IStatusResponse> DeleteAsync(DeletePersonRequest id, CancellationToken token = default);
    }
}
