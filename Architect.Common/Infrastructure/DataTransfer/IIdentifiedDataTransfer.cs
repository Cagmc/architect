using System.ComponentModel.DataAnnotations;

namespace Architect.Common.Infrastructure.DataTransfer
{
    public interface IIdentifiedDataTransfer
    {
        [Range(1, int.MaxValue)]
        int Id { get; set; }
    }
}
