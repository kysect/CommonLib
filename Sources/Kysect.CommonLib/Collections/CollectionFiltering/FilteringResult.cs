namespace Kysect.CommonLib.Collections.CollectionFiltering;

public record FilteringResult<T>(bool IsSatisfied, string Message)
{
    public static FilteringResult<T> Ok()
    {
        return new FilteringResult<T>(true, string.Empty);
    }
}