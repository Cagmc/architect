using Architect.Common.Infrastructure;

namespace Architect.PersonFeature.Events
{
    public class UpdateEvent : IEvent
    {
        public UpdateEvent(Database.Entities.Person person)
        {
            Person = person;
        }

        public Database.Entities.Person Person { get; }
    }
}
