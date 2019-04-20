using System;
using System.Threading;
using System.Threading.Tasks;

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

        public async Task<object> GetAsync(int id, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public async Task<object> CreateAsync(object model, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public async Task<object> UpdateAsync(object model, CancellationToken token =default)
        {
            throw new NotImplementedException();
        }

        public async Task<object> DeleteAsync(int id, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }
    }
}
