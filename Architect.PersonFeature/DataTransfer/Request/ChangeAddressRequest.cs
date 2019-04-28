using Architect.Common.Infrastructure.DataTransfer.Request;

namespace Architect.PersonFeature.DataTransfer.Request
{
    public class ChangeAddressRequest : UpdateRequestBase
    {
        public ChangeAddressRequest(int id) : base(id)
        {

        }

        public AddressRequest Address { get; set; }
    }
}
