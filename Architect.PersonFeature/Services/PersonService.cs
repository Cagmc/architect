using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Architect.Common.Infrastructure;
using Architect.Common.Infrastructure.DataTransfer.Request;
using Architect.Common.Infrastructure.DataTransfer.Response;
using Architect.Database;
using Architect.Database.Entities;
using Architect.PersonFeature.DataTransfer.Request;
using Architect.PersonFeature.DataTransfer.Response;
using Architect.PersonFeature.Queries;
using Microsoft.EntityFrameworkCore;

namespace Architect.PersonFeature.Services
{
    public class PersonService : DomainServiceBase<DatabaseContext, Person, PersonAggregate>, IPersonService
    {
        public PersonService(DatabaseContext context, PersonStore store, IEventDispatcher eventDispatcher)
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

            context.Set<Person>().Add(entity);

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
                context.Set<Person>().Remove(entity);
                context.Set<Address>().Remove(entity.Address);
                context.Set<Name>().Remove(entity.Name);

                await context.SaveChangesAsync(token);
                await eventDispatcher.DispatchAsync(new Events.DeleteEvent(entity));

                response = new StatusResponse(model.Id);
            }

            return response;
        }

        public virtual async Task<IDataResponse<PersonViewModel>> GetAsync(
            GetRequest request, CancellationToken token = default)
        {
            request.ArgumentNullCheck(nameof(request));
            request.Id.ArgumentOutOfRangeCheck(nameof(request.Id));

            var entity = await store.GetEntityAsync(request.Id, token);

            IDataResponse<PersonViewModel> response;
            if (entity == null)
            {
                response = NotFoundDataResponse<PersonViewModel>(request.Id);
            }
            else
            {
                var viewModel = new PersonViewModel(entity.Id, entity);
                response = new DataResponse<PersonViewModel>(viewModel, request.Id);
            }

            return response;
        }

        public virtual async Task<IListResponse<PersonOverviewViewModel>> GetListAsync(
            PaginationFilter filter, CancellationToken token = default)
        {
            filter.ArgumentNullCheck(nameof(filter));

            var query = context.Set<Person>().AsQueryable();

            var totalCount = await query.CountAsync(token).ConfigureAwaitFalse();

            var items = await query
                .SelectPersonOverviewViewModel()
                .Paginate(filter)
                .ToListAsync(token).ConfigureAwaitFalse();

            return new ListResponse<PersonOverviewViewModel>(items, totalCount, 
                filter.Page ?? 1, totalCount.CountPages(filter.PageSize));
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
