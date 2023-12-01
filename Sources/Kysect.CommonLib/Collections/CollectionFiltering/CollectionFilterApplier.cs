using Kysect.CommonLib.BaseTypes.Extensions;
using Microsoft.Extensions.Logging;

namespace Kysect.CommonLib.Collections.CollectionFiltering;

public class CollectionFilterApplier<T>
{
    private readonly IReadOnlyCollection<ICollectionFilterCondition<T>> _conditions;
    private readonly ILogger _logger;

    public CollectionFilterApplier(IReadOnlyCollection<ICollectionFilterCondition<T>> conditions, ILogger logger)
    {
        _logger = logger;
        _conditions = conditions;
    }

    public CollectionFilterApplyResult<T> Apply(IReadOnlyCollection<T> elements)
    {
        elements.ThrowIfNull();

        var satisfied = new List<T>();
        var logReasonAggregator = new LogReasonAggregator<T>(_logger);

        foreach (T element in elements)
        {
            foreach (ICollectionFilterCondition<T> condition in _conditions)
            {
                FilteringResult<T> filteringResult = condition.IsSatisfied(element);
                if (!filteringResult.IsSatisfied)
                    logReasonAggregator.Add(filteringResult.Message, element);
            }

            satisfied.Add(element);
        }

        return new CollectionFilterApplyResult<T>(logReasonAggregator, satisfied);
    }
}