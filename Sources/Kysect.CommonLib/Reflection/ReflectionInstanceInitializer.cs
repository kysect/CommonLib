using Kysect.CommonLib.Reflection.TypeCache;
using System.Reflection;

namespace Kysect.CommonLib.Reflection;

public class ReflectionInstanceInitializer<T> where T : notnull
{
    private readonly T _instance;
    private readonly PropertyInfo[] _properties;

    public ReflectionInstanceInitializer()
    {
        _instance = TypeInstanceCache<T>.CreateEmptyInstance();
        _properties = TypeInstanceCache<T>.GetPublicProperties();
    }

    public bool Set(string name, object value)
    {
        PropertyInfo? mappedProperty = _properties.SingleOrDefault(p => p.Name == name);
        return ReflectionInstanceInitializingExtensions.TrySetToProperty(_instance, mappedProperty, value);
    }

    public T GetValue()
    {
        return _instance;
    }
}