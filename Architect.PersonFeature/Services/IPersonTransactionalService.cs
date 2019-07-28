using Architect.Common.Infrastructure;
using Architect.PersonFeature.DataTransfer.Request;
using Architect.PersonFeature.DataTransfer.Response;

namespace Architect.PersonFeature.Services
{
    public interface IPersonTransactionalService 
        : ISimpleTransactionalService<IPersonService, PersonViewModel, PersonOverviewViewModel, CreatePersonRequest, UpdatePersonRequest, DeletePersonRequest>,
        IPersonService
    {
    }
}
