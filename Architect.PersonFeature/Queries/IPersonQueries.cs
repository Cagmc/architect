using System.Threading;
using System.Threading.Tasks;

using Architect.Common.Infrastructure.DataTransfer.Request;
using Architect.Common.Infrastructure.DataTransfer.Response;
using Architect.Database.Entities;

namespace Architect.PersonFeature.Queries
{
    public interface IPersonQueries
    {
        Task<IDataResponse<PersonAggregate>> GetAsync(
            int id, CancellationToken token = default);

        Task<IListResponse<PersonAggregate>> GetAsync(
            PaginationFilter filter, CancellationToken token = default);
    }
}
