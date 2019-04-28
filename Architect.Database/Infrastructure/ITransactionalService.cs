using Architect.Common.Infrastructure.DataTransfer.Request;
using Architect.Common.Infrastructure.DataTransfer.Response;

namespace Architect.Database.Infrastructure
{
    public interface ITransactionalService<TService, TViewModel, TCreate, TUpdate, TDelete>  : IService<TViewModel, TCreate, TUpdate, TDelete>
        where TService: IService<TViewModel, TCreate, TUpdate, TDelete>
        where TViewModel : ViewModelBase
        where TCreate : CreateRequestBase
        where TUpdate : UpdateRequestBase
        where TDelete : DeleteRequestBase
    {
    }
}
