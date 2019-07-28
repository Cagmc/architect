using System;
using System.Threading;
using System.Threading.Tasks;

using Architect.Common.Infrastructure;
using Architect.Common.Infrastructure.DataTransfer.Response;
using Architect.PersonFeature.DataTransfer.Request;
using Architect.PersonFeature.DataTransfer.Response;

namespace Architect.PersonFeature.Services
{
    public class PersonTransactionalService 
        : SimpleTransactionalService<IPersonService, PersonViewModel, PersonOverviewViewModel, CreatePersonRequest, UpdatePersonRequest, DeletePersonRequest>, 
        IPersonTransactionalService
    {
        public PersonTransactionalService(IPersonService service) : base(service)
        {
        }

        public virtual async Task<IStatusResponse> ChangeAddressAsync(
            ChangeAddressRequest model, CancellationToken token = default)
        {
            model.ArgumentNullCheck(nameof(model));

            using (var scope = TransactionFactory.CreateTransaction())
            {
                var result = await service.ChangeAddressAsync(model, token)
                    .ConfigureAwaitFalse();
                scope.Complete();

                return result;
            }
        }

        public virtual async Task<IStatusResponse> ChangeNameAsync(
            ChangeNameRequest model, CancellationToken token = default)
        {
            model.ArgumentNullCheck(nameof(model));

            using (var scope = TransactionFactory.CreateTransaction())
            {
                var result = await service.ChangeNameAsync(model, token)
                    .ConfigureAwaitFalse();
                scope.Complete();

                return result;
            }
        }
    }
}
