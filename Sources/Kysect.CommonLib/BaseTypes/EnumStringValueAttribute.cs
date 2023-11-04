namespace Kysect.CommonLib.BaseTypes;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
public sealed class EnumStringValueAttribute : Attribute
{
    public string StringValue { get; }

    public EnumStringValueAttribute(string stringValue)
    {
        StringValue = stringValue;
    }
}