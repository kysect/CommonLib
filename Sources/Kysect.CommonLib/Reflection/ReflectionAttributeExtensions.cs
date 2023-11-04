using Kysect.CommonLib.BaseTypes.Extensions;
using Kysect.CommonLib.Reflection.TypeCache;
using System.Reflection;

namespace Kysect.CommonLib.Reflection;

public class ReflectionAttributeFinder
{
    public bool HasAttribute<TAttribute>(Type type) where TAttribute : Attribute
    {
        type.ThrowIfNull();

        return FindAttributeFromType<TAttribute>(type) is not null;
    }

    public TAttribute? FindAttributeFromType<TAttribute>(Type type) where TAttribute : Attribute
    {
        type.ThrowIfNull();

        return TryExtractAttribute<TAttribute>(type);
    }

    public TAttribute GetAttributeFromType<TAttribute>(Type type) where TAttribute : Attribute
    {
        type.ThrowIfNull();

        TAttribute? customAttribute = FindAttributeFromType<TAttribute>(type);
        if (customAttribute is null)
            throw new ArgumentException($"Type {type.Name} does not contains attribute {typeof(TAttribute).Name}");

        return customAttribute;
    }

    public TAttribute? FindAttributeFromInstance<TAttribute>(object value) where TAttribute : Attribute
    {
        value.ThrowIfNull();

        return FindAttributeFromType<TAttribute>(value.GetType());

    }

    public TAttribute GetAttributeFromInstance<TAttribute>(object value) where TAttribute : Attribute
    {
        value.ThrowIfNull();

        return GetAttributeFromType<TAttribute>(value.GetType());
    }

    public TAttribute GetAttributeFromEnumValue<TAttribute, TEnum>(TEnum value)
        where TAttribute : Attribute
        where TEnum : struct, Enum
    {
        TAttribute? result = FindAttributeFromEnumValue<TAttribute, TEnum>(value);
        return result ?? throw new ArgumentNullException($"Attribute {nameof(TAttribute)} was not found for {value.GetType()}");
    }

    public TAttribute? FindAttributeFromEnumValue<TAttribute, TEnum>(TEnum value)
        where TAttribute : Attribute
        where TEnum : struct, Enum
    {
        MemberInfo member = ExtractEnumValueMember(value);
        return TryExtractAttribute<TAttribute>(member);
    }

    public IReadOnlyCollection<Type> GetTypeWithAttribute<T>(Assembly assembly) where T : Attribute
    {
        assembly.ThrowIfNull();

        return assembly
            .GetTypes()
            .Where(t => t.GetCustomAttributesData().Any(a => a.AttributeType == TypeInstanceCache<T>.Instance))
            .ToList();
    }

    public IReadOnlyDictionary<TEnum, TAttribute> GetAttributesFromEnumValues<TAttribute, TEnum>()
        where TAttribute : Attribute
        where TEnum : struct, Enum
    {
        var result = new Dictionary<TEnum, TAttribute>();

        Type enumType = TypeInstanceCache<TEnum>.Instance;

        foreach (MemberInfo memberInfo in enumType.GetMembers())
        {
            var attribute = TryExtractAttribute<TAttribute>(memberInfo);
            if (attribute is null)
                continue;

            if (!Enum.TryParse<TEnum>(memberInfo.Name, out var enumValue))
                throw new ReflectionException($"Member {memberInfo.Name} cannot be converted to value of enum {typeof(TEnum).Name}");

            result[enumValue] = attribute;
        }

        return result;
    }

    private MemberInfo ExtractEnumValueMember<TEnum>(TEnum value)
    {
        value.ThrowIfNull();

        Type enumType = TypeInstanceCache<TEnum>.Instance;
        MemberInfo[] members = enumType.GetMember(value.ToString());
        if (members.Length != 1)
            throw new ReflectionException($"Unexpected count of member with name {value} in type {enumType.FullName}: {members.Length}");

        MemberInfo member = members.Single();
        return member;
    }

    private TAttribute? TryExtractAttribute<TAttribute>(MemberInfo value) where TAttribute : Attribute
    {
        Type attributeType = TypeInstanceCache<TAttribute>.Instance;
        var customAttribute = Attribute.GetCustomAttribute(value, attributeType);
        return (TAttribute) customAttribute;
    }
}