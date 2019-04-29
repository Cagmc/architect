using Architect.Common.Infrastructure;

namespace Architect.PersonFeature.Events
{
    public class DeleteEvent : IEvent
    {
        public DeleteEvent(Database.Entities.Person person)
        {
            Person = person;
        }

        public Database.Entities.Person Person { get; }
    }
}
