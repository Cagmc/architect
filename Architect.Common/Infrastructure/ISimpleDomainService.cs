using Architect.Common.Infrastructure.DataTransfer.Request;
using Architect.Common.Infrastructure.DataTransfer.Response;

namespace Architect.Common.Infrastructure
{
    public interface ISimpleDomainService<TDetailedViewModel, TOverviewViewModel, TCreate, TUpdate, TDelete>
            : IDomainService<TDetailedViewModel, TOverviewViewModel, PaginationFilter, GetRequest, TCreate, TUpdate, TDelete>
        where TDetailedViewModel : DetailedViewModelBase
        where TOverviewViewModel : OverviewViewModelBase
        where TCreate : CreateRequestBase
        where TUpdate : UpdateRequestBase
        where TDelete : DeleteRequestBase
    {

    }
}
