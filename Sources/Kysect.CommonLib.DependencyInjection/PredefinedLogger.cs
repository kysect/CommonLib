using Microsoft.Extensions.Logging;

namespace Kysect.CommonLib.DependencyInjection;

public static class PredefinedLogger
{
    public static ILogger CreateConsoleLogger(string category = "ConsoleLogger", LogLevel logLevel = LogLevel.Trace)
    {
        using ILoggerFactory factory = LoggerFactory
            .Create(b =>
            {
                b
                    .AddFilter(null, logLevel)
                    .AddSimpleConsole(options =>
                    {
                        options.IncludeScopes = true;
                        options.SingleLine = true;
                        options.TimestampFormat = "HH:mm:ss ";
                    });
            });

        return factory.CreateLogger(category);
    }
}