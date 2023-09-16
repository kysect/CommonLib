using Microsoft.Extensions.Logging;

namespace Kysect.CommonLib.Logging;

public static class LoggerTabExtensions
{
    public static void LogTabError(this ILogger logger, int tabCount, string text)
    {
        string prefix = GenerateStringWithTab(tabCount);
        logger.LogError("{prefix}{message}", prefix, text);
    }

    public static void LogTabInformation(this ILogger logger, int tabCount, string text)
    {
        string prefix = GenerateStringWithTab(tabCount);
        logger.LogInformation("{prefix}{message}", prefix, text);
    }

    public static void LogTabDebug(this ILogger logger, int tabCount, string text)
    {
        string prefix = GenerateStringWithTab(tabCount);
        logger.LogDebug("{prefix}{message}", prefix, text);
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