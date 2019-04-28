using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

using Architect.Common.Infrastructure.DataTransfer.Response;
using Architect.PersonFeature.DataTransfer.Request;
using Architect.PersonFeature.DataTransfer.Response;

namespace Architect.PersonFeature.Services
{
    public class PersonTransactionalService : IPersonTransactionalService
    {
        protected readonly IPersonService service;

        public PersonTransactionalService(IPersonService service)
        {
            this.service = service;
        }

        public virtual async Task<IStatusResponse> CreateAsync(
            CreatePersonRequest model, CancellationToken token = default)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                var result = await service.CreateAsync(model, token);
                scope.Complete();

                return result;
            }
        }

        public virtual async Task<IStatusResponse> DeleteAsync(
            DeletePersonRequest model, CancellationToken token = default)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                var result = await service.DeleteAsync(model, token);
                scope.Complete();

                return result;
            }
        }

        public virtual async Task<IDataResponse<PersonViewModel>> GetAsync(
            int id, CancellationToken token = default)
        {
            var result = await service.GetAsync(id, token);

            return result;
        }

        public virtual async Task<IStatusResponse> UpdateAsync(
            UpdatePersonRequest model, CancellationToken token = default)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                var result = await service.UpdateAsync(model, token);
                scope.Complete();

                return result;
            }
        }
    }
}
