using Kysect.CommonLib.BaseTypes.Extensions;
using Kysect.CommonLib.Disposing;
using Microsoft.Extensions.Logging;
using System.Text;

namespace Kysect.CommonLib.Tests.Logging.Fakes;

public class StringBuilderLogger : ILogger
{
    private readonly StringBuilder _stringBuilder;

    public StringBuilderLogger()
    {
        _stringBuilder = new StringBuilder();
    }


    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        state.ThrowIfNull();

        _stringBuilder.AppendLine(state.ToString());
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return true;
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return new DummyDisposable();
    }

    public string Build()
    {
        return _stringBuilder.ToString();
    }
}