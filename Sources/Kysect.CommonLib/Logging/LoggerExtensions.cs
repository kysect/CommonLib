using Microsoft.Extensions.Logging;

namespace Kysect.CommonLib.Logging;

public static class LoggerExtensions
{
    public static ILogger WithPrefix(this ILogger logger, string prefix)
    {
        return new PrefixLoggerProxy(logger, prefix);
    }

    public static ILogger WithPrefix<T>(this ILogger logger)
    {
        return new PrefixLoggerProxy(logger, typeof(T).Name);
    }
}