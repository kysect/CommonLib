using Kysect.CommonLib.BaseTypes.Extensions;

namespace Kysect.CommonLib.Collections.Extensions;

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

    public static TimeSpan? Sum(this IEnumerable<TimeSpan?> values)
    {
        values.ThrowIfNull();

        TimeSpan? result = null;

        foreach (TimeSpan? value in values)
        {
            if (value is null)
                continue;

            result = result?.Add(value.Value) ?? value;
        }

        return result;
    }

    public static TimeSpan? Sum<T>(this IEnumerable<T> values, Func<T, TimeSpan?> selector)
    {
        values.ThrowIfNull();
        selector.ThrowIfNull();

        TimeSpan? result = null;

        foreach (T value in values)
        {
            if (value is null)
                continue;

            TimeSpan? selectedValue = selector(value);
            if (selectedValue is null)
                continue;

            result = result?.Add(selectedValue.Value) ?? selectedValue;
        }

        return result;
    }

    public static IReadOnlyCollection<T> CloneCollection<T>(this IEnumerable<T> source)
    {
        return source.Select(s => s).ToList();
    }

    public static IEnumerable<T> DistinctByForLegacy<T, T2>(this IEnumerable<T> values, Func<T, T2> selector)
    {
        values.ThrowIfNull();
        selector.ThrowIfNull();

        var uniqueValue = new HashSet<T2>();

        foreach (T value in values)
        {
            if (uniqueValue.Add(selector(value)))
                yield return value;
        }
    }
}