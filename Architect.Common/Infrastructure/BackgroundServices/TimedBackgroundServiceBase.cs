using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Architect.Common.Infrastructure.BackgroundServices
{
    public abstract class TimedBackgroundServiceBase<TOptions> : ITimedBackgroundService, IDisposable
        where TOptions: ITimedOptions
    {
        protected readonly IServiceProvider services;
        protected readonly TOptions options;
        protected readonly ILogger logger;
        private Timer timer;

        public TimedBackgroundServiceBase(IServiceProvider services,
            ILogger<TimedBackgroundServiceBase<TOptions>> logger)
        {
            this.services = services.ArgumentNullCheck(nameof(services));
            this.logger = logger.ArgumentNullCheck(nameof(logger));

            options = GetOptions();
        }

        public abstract Task DoWorkAsync(CancellationToken token = default);

        protected virtual async void TimerCallback(object state)
        {
            await DoWorkAsync();
        }

        public virtual Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation($"{GetType().Name} is starting");

            timer = new Timer(
                TimerCallback,
                null, 
                TimeSpan.FromSeconds(options.DelaySeconds),
                TimeSpan.FromSeconds(options.FrequencySeconds));

            return Task.CompletedTask;
        }

        public virtual Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation($"{GetType().Name} is stopping");

            timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        protected virtual TOptions GetOptions()
        {
            using (var scope = services.CreateScope())
            {
                var options =
                    scope.ServiceProvider
                        .GetRequiredService<IOptionsMonitor<TOptions>>();

                if (options == null)
                {
                    logger.LogWarning("options_not_found");
                }

                return options.CurrentValue;
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    timer?.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~TimedBackgroundServiceBase()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }

    public interface ITimedOptions
    {
        int DelaySeconds { get; set; }
        int FrequencySeconds { get; set; }
    }
}
