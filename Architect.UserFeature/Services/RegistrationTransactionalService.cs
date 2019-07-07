using System;
using System.Threading;
using System.Threading.Tasks;

using Architect.Common.Infrastructure;
using Architect.Common.Infrastructure.DataTransfer.Response;
using Architect.UserFeature.DataTransfer.Request;

namespace Architect.UserFeature.Services
{
    public class RegistrationTransactionalService : IRegistrationTransactionalService
    {
        private readonly IRegistrationService service;

        public RegistrationTransactionalService(IRegistrationService service)
        {
            this.service = service.ArgumentNullCheck(nameof(service));
        }

        public async Task<IStatusResponse> RegistrationAsync(
            RegistrationRequest request, CancellationToken token = default)
        {
            request.ArgumentNullCheck(nameof(request));

            using (var scope = TransactionFactory.CreateTransaction())
            {
                var result = await service.RegistrationAsync(request, token)
                    .ConfigureAwait(false);
                scope.Complete();

                return result;
            }
        }
    }
}
