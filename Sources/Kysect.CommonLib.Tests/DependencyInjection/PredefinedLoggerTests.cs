using Kysect.CommonLib.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Kysect.CommonLib.Tests.DependencyInjection;

public class PredefinedLoggerTests
{
    [Test]
    public void CreateLogger_Ok()
    {
        ILogger consoleLogger = PredefinedLogger.CreateConsoleLogger(logLevel: LogLevel.Debug);

        foreach (LogLevel logLevel in Enum.GetValues<LogLevel>())
            consoleLogger.Log(logLevel, "Level: {LogLevel}", logLevel);
    }
}