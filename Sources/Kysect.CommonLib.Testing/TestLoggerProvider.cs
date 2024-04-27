using Microsoft.Extensions.Logging;

#if DEBUG
using Kysect.CommonLib.DependencyInjection.Logging;
#else
using Microsoft.Extensions.Logging.Abstractions;
#endif

namespace Kysect.CommonLib.Testing;

public static class TestLoggerProvider
{
    public static ILogger Provide()
    {
#if DEBUG
        return DefaultLoggerConfiguration.CreateConsoleLogger();
#else
        return NullLogger.Instance;
#endif
    }
}