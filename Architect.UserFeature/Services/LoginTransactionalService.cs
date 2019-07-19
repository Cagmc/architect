using System;
using System.Threading;
using System.Threading.Tasks;

using Architect.Common.Infrastructure;
using Architect.Common.Infrastructure.DataTransfer.Response;
using Architect.UserFeature.DataTransfer.Request;
using Architect.UserFeature.DataTransfer.Response;

namespace Architect.UserFeature.Services
{
    public class LoginTransactionalService : ILoginTransactionalService
    {
        private readonly ILoginService service;

        public LoginTransactionalService(ILoginService service)
        {
            this.service = service.ArgumentNullCheck(nameof(service));
        }

        public async Task<IDataResponse<SelfViewModel>> GetSelfAsync(
            CancellationToken token = default)
        {
            using (var scope = TransactionFactory.CreateTransaction())
            {
                var result = await service.GetSelfAsync(token)
                    .ConfigureAwaitFalse();
                scope.Complete();

                return result;
            }
        }

        public async Task<IDataResponse<SelfViewModel>> LoginAsync(
            LoginRequest model, CancellationToken token = default)
        {
            model.ArgumentNullCheck(nameof(model));

            using (var scope = TransactionFactory.CreateTransaction())
            {
                var result = await service.LoginAsync(model, token)
                    .ConfigureAwaitFalse();
                scope.Complete();

                return result;
            }
        }

        public async Task<IStatusResponse> LogoutAsync(CancellationToken token = default)
        {
            using (var scope = TransactionFactory.CreateTransaction())
            {
                var result = await service.LogoutAsync(token)
                    .ConfigureAwaitFalse();
                scope.Complete();

                return result;
            }
        }
    }
}
