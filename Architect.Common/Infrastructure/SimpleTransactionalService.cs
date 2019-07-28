using Architect.Common.Infrastructure.DataTransfer.Request;
using Architect.Common.Infrastructure.DataTransfer.Response;

namespace Architect.Common.Infrastructure
{
    public class SimpleTransactionalService<TService, TDetailedViewModel, TOverviewViewModel, TCreate, TUpdate, TDelete>
        : TransactionalService<TService, TDetailedViewModel, TOverviewViewModel, PaginationFilter, GetRequest, TCreate, TUpdate, TDelete>,
            ISimpleTransactionalService<TService, TDetailedViewModel, TOverviewViewModel, TCreate, TUpdate, TDelete>
        where TService : class, ISimpleDomainService<TDetailedViewModel, TOverviewViewModel, TCreate, TUpdate, TDelete>
        where TDetailedViewModel : DetailedViewModelBase
        where TOverviewViewModel : OverviewViewModelBase
        where TCreate : CreateRequestBase
        where TUpdate : UpdateRequestBase
        where TDelete : DeleteRequestBase
    {
        public SimpleTransactionalService(TService service) : base(service)
        {
        }
    }
}
