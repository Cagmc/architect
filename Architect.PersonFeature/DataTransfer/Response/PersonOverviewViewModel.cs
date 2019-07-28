using Architect.Common.Infrastructure.DataTransfer.Response;

namespace Architect.PersonFeature.DataTransfer.Response
{
    public class PersonOverviewViewModel : OverviewViewModelBase
    {
        public PersonOverviewViewModel(int id) : base(id)
        {
        }

        public string Name { get; set; }
    }
}
