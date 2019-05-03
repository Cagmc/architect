using System.ComponentModel.DataAnnotations;

namespace Architect.Common.Infrastructure.DataTransfer.Request
{
    public abstract class DeleteRequestBase : RequestBase, IIdentifiedDataTransfer
    {
        public DeleteRequestBase(int id)
        {
            Id = id;
        }

        [Range(1, int.MaxValue)]
        public virtual int Id { get; set; }
    }
}
