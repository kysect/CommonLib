using Kysect.CommonLib.BaseTypes.Extensions;
using Kysect.CommonLib.Disposing;
using Microsoft.Extensions.Logging;

namespace Kysect.CommonLib.Tests.Logging.Fakes;

public class StringBuilderLogger : ILogger
{
    private readonly List<string> _logLines;

    public StringBuilderLogger()
    {
        _logLines = new List<string>();
    }


    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        state.ThrowIfNull();

        string? logLine = state.ToString();
        logLine.ThrowIfNull();
        _logLines.Add(logLine);
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return true;
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