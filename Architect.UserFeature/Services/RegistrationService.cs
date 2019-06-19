using Architect.Common.Infrastructure;
using Architect.Database;

namespace Architect.UserFeature.Services
{
    public class RegistrationService : ServiceBase<DatabaseContext>, IRegistrationService
    {
        public RegistrationService(DatabaseContext context) : base(context)
        {

        }
    }
}
