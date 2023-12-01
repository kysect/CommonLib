namespace Kysect.CommonLib.Collections.CollectionFiltering;

public record struct CollectionFilterApplyResult<T>(LogReasonAggregator<T> Reasons, IReadOnlyCollection<T> SatisfiedElements);