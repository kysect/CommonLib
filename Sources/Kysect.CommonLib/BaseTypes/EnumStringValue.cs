using Kysect.CommonLib.BaseTypes.Extensions;
using Kysect.CommonLib.Reflection;

namespace Kysect.CommonLib.BaseTypes;

public static class EnumStringValue
{
    public static string ToEnumString<T>(T value) where T : struct, Enum
    {
        var reflectionAttributeFinder = new ReflectionAttributeFinder();

        EnumStringValueAttribute enumStringValueAttribute = reflectionAttributeFinder.GetAttributeFromEnumValue<EnumStringValueAttribute, T>(value);

        return enumStringValueAttribute.StringValue;
    }

    public static T FromEnumString<T>(string value) where T : struct, Enum
    {
        value.ThrowIfNull();

        var reflectionAttributeFinder = new ReflectionAttributeFinder();

        IReadOnlyDictionary<T, EnumStringValueAttribute> enumStrings = reflectionAttributeFinder.GetAttributesFromEnumValues<EnumStringValueAttribute, T>();

        KeyValuePair<T, EnumStringValueAttribute>? selectedEnumValue = enumStrings.FirstOrDefault(p => p.Value.StringValue == value);

        if (selectedEnumValue is null)
            throw new ReflectionException($"Cannot find enum values associated to string {value} in {typeof(T).Name}");

        return selectedEnumValue.Value.Key;
    }
}