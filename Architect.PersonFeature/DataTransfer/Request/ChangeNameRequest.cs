using Architect.Common.Infrastructure.DataTransfer.Request;

namespace Architect.PersonFeature.DataTransfer.Request
{
    public class ChangeNameRequest : UpdateRequestBase
    {
        public ChangeNameRequest(int id) : base(id)
        {

        }

        public NameRequest Name { get; set; }
    }
}
