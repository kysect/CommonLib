using Microsoft.Extensions.Logging;

namespace Kysect.CommonLib.DependencyInjection.Logging;

public static class DefaultLoggerConfiguration
{
    public static ILogger CreateConsoleLogger(string category = "ConsoleLogger", LogLevel logLevel = LogLevel.Trace)
    {
        using var logConfigurationBuilder = new LogConfigurationBuilder();

        return logConfigurationBuilder
            .SetLevel(logLevel)
            .SetDefaultCategory(category)
            .AddSpectreConsole()
            .Build();
    }

    public static ILogger CreateSpectreConsoleAndFile(string logDirectory, string category, LogLevel logLevel)
    {
        using var logConfigurationBuilder = new LogConfigurationBuilder();

        return logConfigurationBuilder
            .SetLevel(logLevel)
            .SetRedirectToAppData(logDirectory)
            .SetDefaultCategory(category)
            .AddSpectreConsole()
            .AddSerilogToFile($"{category}.log")
            .Build();
    }
}