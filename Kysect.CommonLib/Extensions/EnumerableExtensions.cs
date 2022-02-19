namespace Kysect.CommonLib;

public static class EnumerableExtensions
{
    public static string ToSingleString<T>(this IEnumerable<T> elements, Func<T, string> selector, string delimiter = ", ")
    {
        return elements
            .Select(selector)
            .ToSingleString(delimiter);
    }

    public static string ToSingleString<T>(this IEnumerable<T> elements, string delimiter = ", ")
    {
        return string.Join(delimiter, elements);
    }
}