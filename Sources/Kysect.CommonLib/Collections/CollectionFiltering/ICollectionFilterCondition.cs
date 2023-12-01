namespace Kysect.CommonLib.Collections.CollectionFiltering;

public interface ICollectionFilterCondition<T>
{
    FilteringResult<T> IsSatisfied(T element);
}