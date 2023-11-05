using System.Runtime.CompilerServices;

namespace Kysect.CommonLib.Exceptions;

public static class SwitchDefaultExceptions
{
    public static ArgumentOutOfRangeException OnUnexpectedValue<T>(T? value, [CallerArgumentExpression(nameof(value))] string argumentName = "")
    {
        return new ArgumentOutOfRangeException(argumentName, $"Value {value} is not expected");
    }

    public static ArgumentOutOfRangeException OnUnexpectedType<T>(T value, [CallerArgumentExpression(nameof(value))] string argumentName = "")
    {
        if (value is null)
            return new ArgumentOutOfRangeException(argumentName, $"Null value is not expected");

        return new ArgumentOutOfRangeException(argumentName, $"Type {value.GetType().FullName} is not expected");
    }
}