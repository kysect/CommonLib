using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Text;

namespace Kysect.CommonLib.Logging;

public class LoggerTraceListener : TraceListener
{
    private readonly ILoggerFactory _loggerFactory;
    private readonly ILogger _defaultLogger;
    private readonly ConcurrentDictionary<string, ILogger> _loggers = new();
    private readonly StringBuilder _builder = new();

    public LoggerTraceListener(ILoggerFactory loggerFactory)
    {
        _loggerFactory = loggerFactory;
        _defaultLogger = loggerFactory.CreateLogger(nameof(LoggerTraceListener));
    }

    public override void TraceEvent(TraceEventCache? eventCache, string source, TraceEventType eventType, int id, string? message)
    {
        ILogger logger = _loggers.GetOrAdd(source, _loggerFactory.CreateLogger);

        logger.Log(MapLevel(eventType), message);
    }

    public override void TraceEvent(TraceEventCache? eventCache, string source, TraceEventType eventType, int id, string? format,
        params object?[]? args)
    {
        ILogger logger = _loggers.GetOrAdd(source, _loggerFactory.CreateLogger);

        logger.Log(MapLevel(eventType), format, args ?? Array.Empty<object>());
    }

    public override void Write(string? message)
    {
        _builder.Append(message);
    }

    public override void WriteLine(string? message)
    {
        _builder.AppendLine(message);
        _defaultLogger.LogInformation(_builder.ToString());
        _builder.Clear();
    }

    private LogLevel MapLevel(TraceEventType eventType) => eventType switch
    {
        TraceEventType.Verbose => LogLevel.Debug,
        TraceEventType.Information => LogLevel.Information,
        TraceEventType.Critical => LogLevel.Critical,
        TraceEventType.Error => LogLevel.Error,
        TraceEventType.Warning => LogLevel.Warning,
        TraceEventType.Resume => throw new NotSupportedException(),
        TraceEventType.Start => throw new NotSupportedException(),
        TraceEventType.Stop => throw new NotSupportedException(),
        TraceEventType.Suspend => throw new NotSupportedException(),
        TraceEventType.Transfer => throw new NotSupportedException(),
        _ => LogLevel.Trace
    };
}