using Architect.Common.Infrastructure.DataTransfer.Request;
using Architect.Common.Infrastructure.DataTransfer.Response;

namespace Architect.Common.Infrastructure
{
    public interface ITransactionalService<TService, TDetailedViewModel, TOverviewViewModel, TListFilter, TGet, TCreate, TUpdate, TDelete>
            : IDomainService<TDetailedViewModel, TOverviewViewModel, TListFilter, TGet, TCreate, TUpdate, TDelete>
        where TService : IDomainService<TDetailedViewModel, TOverviewViewModel, TListFilter, TGet, TCreate, TUpdate, TDelete>
        where TDetailedViewModel : DetailedViewModelBase
        where TOverviewViewModel : OverviewViewModelBase
        where TListFilter : PaginationFilter
        where TGet : GetRequest
        where TCreate : CreateRequestBase
        where TUpdate : UpdateRequestBase
        where TDelete : DeleteRequestBase
    {
    }
}
