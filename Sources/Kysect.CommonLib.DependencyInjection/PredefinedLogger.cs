using Microsoft.Extensions.Logging;

namespace Kysect.CommonLib.DependencyInjection;

public static class PredefinedLogger
{
    public static ILogger CreateConsoleLogger(string category = "ConsoleLogger")
    {
        ILoggerFactory factory = LoggerFactory
            .Create(b =>
            {
                b.AddSimpleConsole(options =>
                {
                    options.IncludeScopes = true;
                    options.SingleLine = true;
                    options.TimestampFormat = "HH:mm:ss ";
                });
            });

        return factory.CreateLogger(category);
    }
}