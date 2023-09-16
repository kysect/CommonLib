namespace Kysect.CommonLib.Reflection;

public class ReflectionException : Exception
{
    public ReflectionException()
    {
    }

    public ReflectionException(string message) : base(message)
    {
    }

    public ReflectionException(string message, Exception exception) : base(message, exception)
    {
    }
}