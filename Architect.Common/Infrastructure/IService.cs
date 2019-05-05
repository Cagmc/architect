using System.Threading;
using System.Threading.Tasks;

using Architect.Common.Infrastructure.DataTransfer.Request;
using Architect.Common.Infrastructure.DataTransfer.Response;

namespace Architect.Common.Infrastructure
{
    public interface IService<TViewModel, TCreate, TUpdate, TDelete>
        where TViewModel : ViewModelBase
        where TCreate : CreateRequestBase
        where TUpdate : UpdateRequestBase
        where TDelete : DeleteRequestBase
    {
        Task<IDataResponse<TViewModel>> GetAsync(int id, CancellationToken token = default);
        Task<IStatusResponse> CreateAsync(TCreate model, CancellationToken token = default);
        Task<IStatusResponse> UpdateAsync(TUpdate model, CancellationToken token = default);
        Task<IStatusResponse> DeleteAsync(TDelete model, CancellationToken token = default);
    }
}
