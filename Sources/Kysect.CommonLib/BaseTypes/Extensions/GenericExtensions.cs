using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Kysect.CommonLib.BaseTypes.Extensions;

public static class GenericExtensions
{
    [return: NotNullIfNotNull(nameof(source))]
    public static T? To<T>(this object? source)
    {
        try
        {
            if (source is null)
                return default;

            return (T) source;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Cannot convert {source?.GetType()} to {typeof(T)}", ex);
        }
    }

    public static TResult To<T, TResult>(this T source, Func<T, TResult> selector)
    {
        selector.ThrowIfNull();

        return selector(source);
    }

    [return: NotNull]
    public static T ThrowIfNull<T>([NotNull] this T value, [CallerArgumentExpression(nameof(value))] string argumentName = "")
    {
        if (value == null)
            throw new ArgumentNullException(argumentName);

        return value;
    }
}