using Architect.Common.Infrastructure;

namespace Architect.PersonFeature.Events
{
    public class CreateEvent : IEvent
    {
        public CreateEvent(Database.Entities.Person person)
        {
            Person = person;
        }

        public Database.Entities.Person Person { get; }
    }
}
