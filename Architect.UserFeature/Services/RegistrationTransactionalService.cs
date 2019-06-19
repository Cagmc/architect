using Architect.Common.Infrastructure;
using Architect.Database;

namespace Architect.UserFeature.Services
{
    public class RegistrationTransactionalService : ServiceBase<DatabaseContext>, IRegistrationTransactionalService
    {
        public RegistrationTransactionalService(DatabaseContext context) : base(context)
        {

        }
    }
}
