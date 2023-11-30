using Kysect.CommonLib.DependencyInjection.Logging;
using Microsoft.Extensions.Logging;

namespace Kysect.CommonLib.Tests.DependencyInjection;

public class DefaultLoggerConfigurationTests
{
    [Test]
    public void CreateLogger_Ok()
    {
        ILogger consoleLogger = DefaultLoggerConfiguration.CreateConsoleLogger(logLevel: LogLevel.Debug);

        foreach (LogLevel logLevel in Enum.GetValues<LogLevel>())
            consoleLogger.Log(logLevel, "Level: {LogLevel}", logLevel);
    }
}