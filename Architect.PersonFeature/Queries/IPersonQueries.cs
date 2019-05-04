using System.Threading;
using System.Threading.Tasks;

using Architect.Common.Infrastructure.DataTransfer.Response;
using Architect.Database.Entities;
using Architect.PersonFeature.DataTransfer.Request;

namespace Architect.PersonFeature.Queries
{
    public interface IPersonQueries
    {
        Task<IDataResponse<PersonAggregate>> GetAsync(
            int id, CancellationToken token = default);

        Task<IListResponse<PersonAggregate>> GetAsync(
            PeopleFilter filter, CancellationToken token = default);
    }
}
