namespace Kysect.CommonLib.Reflection;

public class ReflectionException : Exception
{
    public ReflectionException(string message, Exception exception) : base(message, exception)
    {
    }
}