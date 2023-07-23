namespace Kysect.CommonLib.Exceptions;

public class SwitchDefaultException
{
    public static ArgumentOutOfRangeException OnUnexpectedEnum<T>(string argumentName, T value) where T : struct, Enum
    {
        return new ArgumentOutOfRangeException(argumentName, $"Type {value} is not expected.");
    }

    public static ArgumentOutOfRangeException OnUnexpectedEnum<T>(string argumentName, T? value) where T : struct, Enum
    {
        return new ArgumentOutOfRangeException(argumentName, $"Type {value} is not expected.");
    }

    public static ArgumentOutOfRangeException OnUnexpectedType<T>(string argumentName, T value)
    {
        return new ArgumentOutOfRangeException(argumentName, $"Type {value?.GetType()} is not expected.");
    }
}