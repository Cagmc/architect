using System.ComponentModel.DataAnnotations;

namespace Architect.Common.Infrastructure.DataTransfer.Response
{
    public abstract class ViewModelBase : IIdentifiedDataTransfer
    {
        public ViewModelBase(int id)
        {
            Id = id;
        }

        [Range(1, int.MaxValue)]
        public int Id { get; set; }
    }
}
