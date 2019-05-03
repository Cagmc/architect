using System;
using System.Threading;
using System.Threading.Tasks;

using Architect.Common.Infrastructure;
using Architect.Common.Infrastructure.DataTransfer.Response;
using Architect.Database.Entities;
using Architect.Database.Infrastructure;
using Architect.PersonFeature.DataTransfer.Request;
using Architect.PersonFeature.DataTransfer.Response;

namespace Architect.PersonFeature.Services
{
    public class PersonService : ServiceBase<Person, PersonAggregate>, IPersonService
    {
        public PersonService(Database.DatabaseContext context, PersonStore store, IEventDispatcher eventDispatcher)
            : base(context, store, eventDispatcher)
        {
        }

        public virtual async Task<IStatusResponse> ChangeAddressAsync(
            ChangeAddressRequest model, CancellationToken token = default)
        {
            model.ArgumentNullCheck(nameof(model));

            var entity = await store.GetEntityAsync(model.Id, token);

            IStatusResponse response;
            if (entity == null)
            {
                response = NotFoundStatusResponse(model.Id);
            }
            else
            {
                model.Address.UpdateEntity(entity.Address);
                await context.SaveChangesAsync(token);

                response = new StatusResponse(entity.Id);
            }

            return response;
        }

        public virtual async Task<IStatusResponse> ChangeNameAsync(
            ChangeNameRequest model, CancellationToken token = default)
        {
            model.ArgumentNullCheck(nameof(model));

            var entity = await store.GetEntityAsync(model.Id, token);

            IStatusResponse response;
            if (entity == null)
            {
                response = NotFoundStatusResponse(model.Id);
            }
            else
            {
                model.Name.UpdateEntity(entity.Name);
                await context.SaveChangesAsync(token);

                response = new StatusResponse(entity.Id);
            }

            return response;
        }

        public virtual async Task<IStatusResponse> CreateAsync(
            CreatePersonRequest model, CancellationToken token = default)
        {
            model.ArgumentNullCheck(nameof(model));

            var entity = model.CreateEntity();

            context.People.Add(entity);

            await context.SaveChangesAsync(token);
            await eventDispatcher.DispatchAsync(new Events.CreateEvent(entity));

            return new StatusResponse(entity.Id);
        }

        public virtual async Task<IStatusResponse> DeleteAsync(
            DeletePersonRequest model, CancellationToken token = default)
        {
            model.ArgumentNullCheck(nameof(model));

            var entity = await store.GetEntityAsync(model.Id, token);

            IStatusResponse response;
            if (entity == null)
            {
                response = NotFoundStatusResponse(model.Id);
            }
            else
            {
                context.People.Remove(entity);
                context.Addresses.Remove(entity.Address);
                context.Names.Remove(entity.Name);

                await context.SaveChangesAsync(token);
                await eventDispatcher.DispatchAsync(new Events.DeleteEvent(entity));

                response = new StatusResponse(model.Id);
            }

            return response;
        }

        public virtual async Task<IDataResponse<PersonViewModel>> GetAsync(
            int id, CancellationToken token = default)
        {
            var entity = await store.GetEntityAsync(id, token);

            IDataResponse<PersonViewModel> response;
            if (entity == null)
            {
                response = NotFoundDataResponse<PersonViewModel>(id);
            }
            else
            {
                var viewModel = new PersonViewModel(entity.Id, entity);
                response = new DataResponse<PersonViewModel>(viewModel, id);
            }

            return response;
        }

        public virtual async Task<IStatusResponse> UpdateAsync(
            UpdatePersonRequest model, CancellationToken token = default)
        {
            model.ArgumentNullCheck(nameof(model));

            var entity = await store.GetEntityAsync(model.Id, token);

            IStatusResponse response;
            if (entity == null)
            {
                response = NotFoundStatusResponse(model.Id);
            }
            else
            {
                model.UpdateEntity(entity);

                await context.SaveChangesAsync(token);
                await eventDispatcher.DispatchAsync(new Events.UpdateEvent(entity));

                response = new StatusResponse(model.Id);
            }

            return response;
        }
    }
}
