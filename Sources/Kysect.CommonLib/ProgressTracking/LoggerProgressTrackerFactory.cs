using Microsoft.Extensions.Logging;

namespace Kysect.CommonLib.ProgressTracking;

public class LoggerProgressTrackerFactory : IProgressTrackerFactory
{
    private readonly ILogger _logger;
    private readonly LogLevel _logLevel;

    public LoggerProgressTrackerFactory(ILogger logger, LogLevel logLevel)
    {
        _logger = logger;
        _logLevel = logLevel;
    }

    public IProgressTracker Create(string operationName, int maxValue)
    {
        return new LoggerProgressTracker(operationName, maxValue, _logger, _logLevel);
    }
}