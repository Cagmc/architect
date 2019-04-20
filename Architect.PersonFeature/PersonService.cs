using System;
using System.Threading;
using System.Threading.Tasks;

using Architect.Common.Infrastructure.DataTransfer.Response;
using Architect.Database.Infrastructure;
using Architect.PersonFeature.DataTransfer.Request;
using Architect.PersonFeature.DataTransfer.Response;

namespace Architect.PersonFeature
{
    public class PersonService : ServiceBase<Database.Entities.Person>, IPersonService
    {
        public PersonService(Database.DatabaseContext context, PersonStore store) : base(context, store)
        {
        }

        public Task<IDataResponse<PersonViewModel>> GetAsync(int id, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public Task<IStatusResponse> CreateAsync(CreatePersonRequest model, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public Task<IStatusResponse> UpdateAsync(UpdatePersonRequest model, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public Task<IStatusResponse> DeleteAsync(DeletePersonRequest id, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }
    }
}
