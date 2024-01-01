using Kysect.CommonLib.BaseTypes.Extensions;
using Kysect.CommonLib.Disposing;
using Microsoft.Extensions.Logging;

namespace Kysect.CommonLib.Logging;

public class StringBuilderLogger : ILogger
{
    private readonly List<string> _logLines;
    private readonly LogLevel _logLevel;

    public StringBuilderLogger(LogLevel logLevel)
    {
        _logLevel = logLevel;
        _logLines = new List<string>();
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel))
        {
            return;
        }

        state.ThrowIfNull();
        formatter.ThrowIfNull();

        string logLine = formatter(state, exception);
        _logLines.Add(logLine);
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return _logLevel <= logLevel;
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return new DummyDisposable();
    }

    public IReadOnlyCollection<string> Build()
    {
        return _logLines;
    }
}