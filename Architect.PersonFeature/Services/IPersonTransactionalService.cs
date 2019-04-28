using Architect.Database.Infrastructure;
using Architect.PersonFeature.DataTransfer.Request;
using Architect.PersonFeature.DataTransfer.Response;

namespace Architect.PersonFeature.Services
{
    public interface IPersonTransactionalService 
        : ITransactionalService<IPersonService, PersonViewModel, CreatePersonRequest, UpdatePersonRequest, DeletePersonRequest>,
        IPersonService
    {
    }
}
