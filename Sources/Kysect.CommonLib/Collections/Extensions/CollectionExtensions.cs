using Kysect.CommonLib.BaseTypes.Extensions;

namespace Kysect.CommonLib.Collections.Extensions;

public static class CollectionExtensions
{
    public static void AddEach<T>(this ICollection<T> collection, IEnumerable<T> elements)
    {
        collection.ThrowIfNull();
        elements.ThrowIfNull();

        foreach (T element in elements)
            collection.Add(element);
    }

    public static bool IsEmpty<T>(this IEnumerable<T> collection)
    {
        return !collection.Any();
    }

    public static void ForEach<T>(this IEnumerable<T> values, Action<T> action)
    {
        values
            .ToList()
            .ForEach(action);
    }

    public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> values)
    {
        return values
            .Where(v => v is not null)
            .Select(v => v!)
            .ToList();
    }
}