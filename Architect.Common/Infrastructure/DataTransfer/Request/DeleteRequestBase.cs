namespace Architect.Common.Infrastructure.DataTransfer.Request
{
    public abstract class DeleteRequestBase : IdentifiedDataTransfer
    {
        public DeleteRequestBase(int id) : base(id)
        {

        }
    }
}
