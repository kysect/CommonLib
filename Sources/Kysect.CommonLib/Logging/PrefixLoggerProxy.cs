﻿using Microsoft.Extensions.Logging;

namespace Kysect.CommonLib.Logging;

public class PrefixLoggerProxy : ILogger
{
    private readonly ILogger _logger;
    private readonly string _prefix;

    public PrefixLoggerProxy(ILogger logger, string prefix)
    {
        _logger = logger;
        _prefix = prefix;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel))
            return;

        _logger.Log(logLevel, eventId, PrepareMessage(state), exception, (s, e) => $"{PrepareMessage(formatter(state, e))}");
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return _logger.IsEnabled(logLevel);
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return _logger.BeginScope(state);
    }

    private string PrepareMessage<TState>(TState state)
    {
        return $"[{_prefix}] {state?.ToString()}";
    }
}