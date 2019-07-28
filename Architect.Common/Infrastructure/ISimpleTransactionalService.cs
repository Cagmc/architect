using Architect.Common.Infrastructure.DataTransfer.Request;
using Architect.Common.Infrastructure.DataTransfer.Response;

namespace Architect.Common.Infrastructure
{
    public interface ISimpleTransactionalService<TService, TDetailedViewModel, TOverviewViewModel, TCreate, TUpdate, TDelete>
            : ISimpleDomainService<TDetailedViewModel, TOverviewViewModel, TCreate, TUpdate, TDelete>
        where TService : ISimpleDomainService<TDetailedViewModel, TOverviewViewModel, TCreate, TUpdate, TDelete>
        where TDetailedViewModel : DetailedViewModelBase
        where TOverviewViewModel : OverviewViewModelBase
        where TCreate : CreateRequestBase
        where TUpdate : UpdateRequestBase
        where TDelete : DeleteRequestBase
    {
    }
}
