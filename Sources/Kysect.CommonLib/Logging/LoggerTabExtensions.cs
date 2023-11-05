using Microsoft.Extensions.Logging;

namespace Kysect.CommonLib.Logging;

public static class LoggerTabExtensions
{
    public static void LogTab(this ILogger logger, LogLevel logLevel, int tabCount, string text)
    {
        string prefix = GenerateStringWithTab(tabCount);
        logger.Log(logLevel, "{prefix}{message}", prefix, text);
    }

    public static void LogTabTrace(this ILogger logger, int tabCount, string text)
    {
        logger.LogTab(LogLevel.Trace, tabCount, text);
    }

    public static void LogTabDebug(this ILogger logger, int tabCount, string text)
    {
        logger.LogTab(LogLevel.Debug, tabCount, text);
    }

    public static void LogTabInformation(this ILogger logger, int tabCount, string text)
    {
        logger.LogTab(LogLevel.Information, tabCount, text);
    }

    public static void LogTabWarning(this ILogger logger, int tabCount, string text)
    {
        logger.LogTab(LogLevel.Warning, tabCount, text);
    }

    public static void LogTabError(this ILogger logger, int tabCount, string text)
    {
        logger.LogTab(LogLevel.Error, tabCount, text);
    }

    public static void LogTabCritical(this ILogger logger, int tabCount, string text)
    {
        logger.LogTab(LogLevel.Critical, tabCount, text);
    }

    private static string GenerateStringWithTab(int tabCount)
    {
        if (tabCount == 0)
            return string.Empty;

        string prefix = string.Empty;

        for (int i = 0; i < tabCount; i++)
            prefix += '\t';

        return prefix;
    }
}