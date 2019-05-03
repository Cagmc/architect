using System.ComponentModel.DataAnnotations;

namespace Architect.Common.Infrastructure.DataTransfer.Request
{
    public abstract class UpdateRequestBase : RequestBase, IIdentifiedDataTransfer
    {
        public UpdateRequestBase(int id)
        {
            Id = id;
        }

        [Range(1, int.MaxValue)]
        public virtual int Id { get; set; }
    }
}
