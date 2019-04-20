using System;

namespace Architect.PersonFeature
{
    public class PersonService : IPersonService
    {
        protected readonly Database.DatabaseContext context;
        protected readonly PersonStore store;

        public PersonService(Database.DatabaseContext context, PersonStore store)
        {
            this.context = context;
            this.store = store;
        }
    }
}
