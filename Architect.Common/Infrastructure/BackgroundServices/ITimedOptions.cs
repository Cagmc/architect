namespace Architect.Common.Infrastructure.BackgroundServices
{
    public interface ITimedOptions
    {
        int FrequencySeconds { get; set; }
        int DelaySeconds { get; set; }
        bool IsEnabled { get; set; }
    }
}
