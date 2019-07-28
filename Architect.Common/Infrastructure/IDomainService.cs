using System.Threading;
using System.Threading.Tasks;

using Architect.Common.Infrastructure.DataTransfer.Request;
using Architect.Common.Infrastructure.DataTransfer.Response;

namespace Architect.Common.Infrastructure
{
    public interface IDomainService<TDetailedViewModel, TOverviewViewModel, TListFilter, TGet, TCreate, TUpdate, TDelete> : IService
        where TDetailedViewModel : DetailedViewModelBase
        where TOverviewViewModel : OverviewViewModelBase
        where TListFilter : PaginationFilter
        where TGet : GetRequest
        where TCreate : CreateRequestBase
        where TUpdate : UpdateRequestBase
        where TDelete : DeleteRequestBase
    {
        Task<IListResponse<TOverviewViewModel>> GetListAsync(TListFilter filter, CancellationToken token = default);
        Task<IDataResponse<TDetailedViewModel>> GetAsync(TGet request, CancellationToken token = default);
        Task<IStatusResponse> CreateAsync(TCreate model, CancellationToken token = default);
        Task<IStatusResponse> UpdateAsync(TUpdate model, CancellationToken token = default);
        Task<IStatusResponse> DeleteAsync(TDelete model, CancellationToken token = default);
    }    
}
