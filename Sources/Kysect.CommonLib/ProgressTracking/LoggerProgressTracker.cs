using Microsoft.Extensions.Logging;

namespace Kysect.CommonLib.ProgressTracking;

public class LoggerProgressTracker : IProgressTracker
{
    private readonly string _operationName;
    private readonly int _maxValue;
    private readonly ILogger _logger;
    private readonly LogLevel _logLevel;

    private int _value;

    public LoggerProgressTracker(string operationName, int maxValue, ILogger logger, LogLevel logLevel)
    {
        _operationName = operationName;
        _maxValue = maxValue;
        _logger = logger;
        _logLevel = logLevel;
    }

    public void OnUpdate()
    {
        Interlocked.Increment(ref _value);
        _logger.Log(_logLevel, "[{Operation}] Processed {Done}/{Max}", _operationName, _value, _maxValue);
    }
}