using System;
using System.Threading;
using System.Threading.Tasks;
using Architect.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Architect.WebApp.BackgroundTasks
{
    internal class ConsumeScopedServiceHostedService : IHostedService, IDisposable
    {
        private readonly ILogger logger;
        private Timer timer;

        public ConsumeScopedServiceHostedService(IServiceProvider services,
            ILogger<ConsumeScopedServiceHostedService> logger)
        {
            Services = services;
            this.logger = logger;
        }

        public IServiceProvider Services { get; }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation(
                "Consume Scoped Service Hosted Service is starting.");

            timer = new Timer(DoWorkAsync, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(5));

            return;
        }

        private async void DoWorkAsync(object state)
        {
            logger.LogInformation(
                "Consume Scoped Service Hosted Service is working.");

            using (var scope = Services.CreateScope())
            {
                var context =
                    scope.ServiceProvider
                        .GetRequiredService<DatabaseContext>();

                var addresses = await context.Addresses.ToListAsync().ConfigureAwait(false);
            }

            for (int i = 0; i < 10; i++)
            {
                using (var scope = Services.CreateScope())
                {
                    var scopedProcessingService =
                        scope.ServiceProvider
                            .GetRequiredService<IScopedProcessingService>();

                    await scopedProcessingService.DoWorkAsync();
                } 
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation(
                "Consume Scoped Service Hosted Service is stopping.");

            timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            timer.Dispose();
        }
    }
}
