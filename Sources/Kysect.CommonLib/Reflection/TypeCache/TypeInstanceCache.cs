using System.Reflection;

namespace Kysect.CommonLib.Reflection.TypeCache;

public static class TypeInstanceCache
{
    private static readonly Dictionary<Type, TypeInstanceCacheElement> _elements = new Dictionary<Type, TypeInstanceCacheElement>();

    public static TypeInstanceCacheElement GetOrAdd(Type type)
    {
        if (!_elements.ContainsKey(type))
        {
            _elements[type] = new TypeInstanceCacheElement(type);
        }

        return _elements[type];
    }
}

public static class TypeInstanceCache<T>
{
    public static Type Instance { get; } = typeof(T);

    public static FieldInfo? FindPropertyBackingFieldName(string propertyName)
    {
        return TypeInstanceCache.GetOrAdd(Instance).FindPropertyBackingFieldName(propertyName);
    }

    public static PropertyInfo[] GetPublicProperties()
    {
        return TypeInstanceCache.GetOrAdd(Instance).GetPublicProperties();
    }

    public static FieldInfo[] GetPrivateFields()
    {
        return TypeInstanceCache.GetOrAdd(Instance).GetPrivateFields();
    }

    public static T CreateEmptyInstance()
    {
        return (T)TypeInstanceCache.GetOrAdd(Instance).CreateEmptyInstance();
    }
}