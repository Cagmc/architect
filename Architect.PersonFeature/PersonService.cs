using System;
using System.Net;
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
        private const string NOT_FOUND = "person_not_found";

        public PersonService(Database.DatabaseContext context, PersonStore store) 
            : base(context, store)
        {
        }

        public async Task<IDataResponse<PersonViewModel>> GetAsync(
            int id, CancellationToken token = default)
        {
            var entity = await store.GetEntityAsync(id, token);

            DataResponse<PersonViewModel> response;
            if (entity == null)
            {
                response = new DataResponse<PersonViewModel>(NOT_FOUND, HttpStatusCode.NotFound, id);
            }
            else
            {
                var viewModel = new PersonViewModel(entity.Id, entity);
                response = new DataResponse<PersonViewModel>(viewModel, id);
            }

            return response;
        }

        public async Task<IStatusResponse> CreateAsync(
            CreatePersonRequest model, CancellationToken token = default)
        {
            model.ArgumentNullCheck(nameof(model));

            var entity = model.CreateEntity();

            context.People.Add(entity);

            await context.SaveChangesAsync(token);

            return new StatusResponse(entity.Id);
        }

        public async Task<IStatusResponse> UpdateAsync(
            UpdatePersonRequest model, CancellationToken token = default)
        {
            model.ArgumentNullCheck(nameof(model));

            var entity = await store.GetEntityAsync(model.Id, token);

            StatusResponse response;
            if (entity == null)
            {
                response = new StatusResponse(NOT_FOUND, HttpStatusCode.NotFound, model.Id);
            }
            else
            {
                model.UpdateEntity(entity);

                await context.SaveChangesAsync(token);

                response = new StatusResponse(model.Id);
            }

            return response;
        }

        public async Task<IStatusResponse> DeleteAsync(
            DeletePersonRequest model, CancellationToken token = default)
        {
            model.ArgumentNullCheck(nameof(model));

            var entity = await store.GetEntityAsync(model.Id, token);

            StatusResponse response;
            if (entity == null)
            {
                response = new StatusResponse(NOT_FOUND, HttpStatusCode.NotFound, model.Id);
            }
            else
            {
                context.People.Remove(entity);
                context.Addresses.Remove(entity.Address);
                context.Names.Remove(entity.Name);

                await context.SaveChangesAsync(token);

                response = new StatusResponse(model.Id);
            }

            return response;
        }
    }
}
