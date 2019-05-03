using Architect.Common.Infrastructure.DataTransfer.Request;

namespace Architect.PersonFeature.DataTransfer.Request
{
    public class DeletePersonRequest : DeleteRequestBase
    {
        public DeletePersonRequest(int id) : base(id)
        {

        }
    }
}
