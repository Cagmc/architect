namespace Architect.Common.Infrastructure.DataTransfer.Request
{
    public abstract class UpdateRequestBase : IdentifiedDataTransfer
    {
        public UpdateRequestBase(int id) : base(id)
        {
        }
    }
}
