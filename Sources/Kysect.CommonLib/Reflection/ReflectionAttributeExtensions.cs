using Kysect.CommonLib.BaseTypes.Extensions;
using Kysect.CommonLib.Reflection.TypeCache;
using System.Reflection;

namespace Kysect.CommonLib.Reflection;

public static class ReflectionAttributeExtensions
{
    public static bool HasAttribute<T>(this Type value) where T : Attribute
    {
        T? customAttribute = FindAttribute<T>(value);
        return customAttribute is not null;
    }

    public static T GetAttribute<T>(this object value) where T : Attribute
    {
        value.ThrowIfNull();

        return GetAttribute<T>(value.GetType());
    }

    public static T GetAttribute<T>(this Type value) where T : Attribute
    {
        value.ThrowIfNull();

        T? customAttribute = FindAttribute<T>(value) ?? throw new ArgumentException($"Type {value.Name} does not contains attribute {typeof(T).Name}");

        return customAttribute;
    }

    public static T? FindAttribute<T>(this Type value) where T : Attribute
    {
        Type attributeType = TypeInstanceCache<T>.Instance;
        var customAttribute = Attribute.GetCustomAttribute(value, attributeType);
        return (T) customAttribute;
    }

    public static IReadOnlyCollection<Type> GetTypeWithAttribute<T>(this Assembly assembly) where T : Attribute
    {
        assembly.ThrowIfNull();

        return assembly
            .GetTypes()
            .Where(t => t.GetCustomAttributesData().Any(a => a.AttributeType == typeof(T)))
            .ToList();
    }

    public static Type[] GetGenericInterfaceInnerTypes(this Type currentType, Type interfaceType)
    {
        currentType.ThrowIfNull();
        interfaceType.ThrowIfNull();

        Type[] allTypeInterfaces = currentType.GetInterfaces();
        Type? concreteTaskInterface = allTypeInterfaces.FirstOrDefault(i =>
            i.IsGenericType &&
            i.GetGenericTypeDefinition() == interfaceType);

        return concreteTaskInterface?.GetGenericArguments() ?? throw new ArgumentException($"Type {currentType.Name} is implement {interfaceType.Name}.");
    }
}