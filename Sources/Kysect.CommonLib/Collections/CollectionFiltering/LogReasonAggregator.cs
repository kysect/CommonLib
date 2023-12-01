using Microsoft.Extensions.Logging;

namespace Kysect.CommonLib.Collections.CollectionFiltering;

public class LogReasonAggregator<T>
{
    private readonly ILogger _logger;

    public Dictionary<string, List<T>> AggregatedLogs { get; }

    public LogReasonAggregator(ILogger logger)
    {
        _logger = logger;
        AggregatedLogs = new Dictionary<string, List<T>>();
    }

    public void Add(string message, T value)
    {
        if (!AggregatedLogs.ContainsKey(message))
            AggregatedLogs[message] = new List<T>();

        AggregatedLogs[message].Add(value);
    }

    public void LogIt()
    {
        foreach (KeyValuePair<string, List<T>> log in AggregatedLogs)
        {
            string reason = log.Key;
            List<T> values = log.Value;
            _logger.LogInformation($"{values.Count} elements with reason: {reason}");
            foreach (T value in values)
            {
                _logger.LogInformation($"Element: {value?.ToString()}");
            }
        }
    }
}