using System.Threading;
using System.Threading.Tasks;

using Architect.Common.Infrastructure.DataTransfer.Response;
using Architect.Database.Entities;
using Architect.PersonFeature.DataTransfer.Request;

namespace Architect.PersonFeature.Queries
{
    public interface IPersonQueries
    {
        Task<IDataResponse<AddressAggregate>> GetAddressAsync(int id, CancellationToken token = default);
        Task<IListResponse<AddressAggregate>> GetAddressAsync(AddressFilter filter, CancellationToken token = default);
        Task<IDataResponse<PersonAggregate>> GetAsync(int id, CancellationToken token = default);
        Task<IListResponse<PersonAggregate>> GetAsync(PeopleFilter filter, CancellationToken token = default);
        Task<IDataResponse<NameAggregate>> GetNameAsync(int id, CancellationToken token = default);
        Task<IListResponse<NameAggregate>> GetNameAsync(NameFilter filter, CancellationToken token = default);
    }
}
