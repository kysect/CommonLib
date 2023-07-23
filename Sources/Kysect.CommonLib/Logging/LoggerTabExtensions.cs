using Microsoft.Extensions.Logging;

namespace Kysect.CommonLib.Logging;

public static class LoggerTabExtensions
{
    public static void LogTabError(this ILogger logger, int tabCount, string text)
    {
        logger.LogError(GenerateStringWithTab(tabCount, text));
    }

    public static void LogTabInformation(this ILogger logger, int tabCount, string text)
    {
        logger.LogInformation(GenerateStringWithTab(tabCount, text));
    }

    public static void LogTabDebug(this ILogger logger, int tabCount, string text)
    {
        logger.LogDebug(GenerateStringWithTab(tabCount, text));
    }

    private static string GenerateStringWithTab(int tabCount, string text)
    {
        if (tabCount == 0)
            return text;

        string prefix = string.Empty;

        for (int i = 0; i < tabCount; i++)
            prefix += '\t';

        return prefix + text;
    }
}