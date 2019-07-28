using System;
using System.Threading;
using System.Threading.Tasks;

using Architect.Common.Infrastructure.DataTransfer.Request;
using Architect.Common.Infrastructure.DataTransfer.Response;

namespace Architect.Common.Infrastructure
{
    public class TransactionalService<TService, TDetailedViewModel, TOverviewViewModel, TListFilter, TGet, TCreate, TUpdate, TDelete> 
        : ITransactionalService<TService, TDetailedViewModel, TOverviewViewModel, TListFilter, TGet, TCreate, TUpdate, TDelete>
        where TService : class, IDomainService<TDetailedViewModel, TOverviewViewModel, TListFilter, TGet, TCreate, TUpdate, TDelete>
        where TDetailedViewModel : DetailedViewModelBase
        where TOverviewViewModel : OverviewViewModelBase
        where TListFilter : PaginationFilter
        where TGet : GetRequest
        where TCreate : CreateRequestBase
        where TUpdate : UpdateRequestBase
        where TDelete : DeleteRequestBase
    {
        protected readonly TService service;

        public TransactionalService(TService service)
        {
            this.service = service.ArgumentNullCheck(nameof(service));
        }

        public virtual async Task<IStatusResponse> CreateAsync(
            TCreate model, CancellationToken token = default)
        {
            model.ArgumentNullCheck(nameof(model));

            using (var scope = TransactionFactory.CreateTransaction())
            {
                var result = await service.CreateAsync(model, token);
                scope.Complete();

                return result;
            }
        }

        public virtual async Task<IStatusResponse> DeleteAsync(
            TDelete model, CancellationToken token = default)
        {
            model.ArgumentNullCheck(nameof(model));

            using (var scope = TransactionFactory.CreateTransaction())
            {
                var result = await service.DeleteAsync(model, token);
                scope.Complete();

                return result;
            }
        }

        public virtual async Task<IDataResponse<TDetailedViewModel>> GetAsync(
            TGet request, CancellationToken token = default)
        {
            request.ArgumentNullCheck(nameof(request));
            request.Id.ArgumentOutOfRangeCheck(nameof(request.Id));

            var result = await service.GetAsync(request, token);

            return result;
        }

        public virtual async Task<IListResponse<TOverviewViewModel>> GetListAsync(
            TListFilter filter, CancellationToken token = default)
        {
            filter.ArgumentNullCheck(nameof(filter));

            var result = await service.GetListAsync(filter, token);

            return result;
        }

        public Task<IStatusResponse> UpdateAsync(TUpdate model, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }
    }
}
