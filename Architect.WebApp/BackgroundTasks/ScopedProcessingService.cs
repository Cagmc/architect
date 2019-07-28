using System.Threading;
using System.Threading.Tasks;

using Architect.Database;

using Microsoft.Extensions.Logging;

namespace Architect.WebApp.BackgroundTasks
{
    internal class ScopedProcessingService : IScopedProcessingService
    {
        private readonly ILogger logger;
        private readonly DatabaseContext context;

        public ScopedProcessingService(DatabaseContext context, ILogger<ScopedProcessingService> logger)
        {
            this.context = context;
            this.logger = logger;
        }

#pragma warning disable
        public async Task DoWorkAsync(CancellationToken token)
#pragma warning enable
        {
            try
            {
                //context.Add(new Label { });
                //await context.SaveChangesAsync(token);
                //logger.LogInformation("Scoped Processing Service is working.");
            }
            catch (System.Exception)
            {
                ;
            }
        }
    }
}
