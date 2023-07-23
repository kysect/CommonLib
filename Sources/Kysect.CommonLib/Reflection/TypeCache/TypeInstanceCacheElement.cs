using System.Reflection;
using System.Runtime.Serialization;

namespace Kysect.CommonLib.Reflection.TypeCache;

public class TypeInstanceCacheElement
{
    public Type Type { get; }

    public TypeInstanceCacheElement(Type type)
    {
        Type = type;
    }

    public FieldInfo? FindPropertyBackingFieldName(string propertyName)
    {
        string backingFieldName = GetBackingFieldName(propertyName);
        return GetPrivateFields().SingleOrDefault(f => f.Name == backingFieldName);
    }

    public string GetBackingFieldName(string propertyName)
    {
        return $"<{propertyName}>k__BackingField";
    }

    public PropertyInfo[] GetPublicProperties()
    {
        return Type.GetProperties();
    }

    public FieldInfo[] GetPrivateFields()
    {
        return Type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
    }

    public object CreateEmptyInstance()
    {
        try
        {
            return FormatterServices.GetUninitializedObject(Type);
        }
        catch (Exception e)
        {
            throw new ReflectionException($"Cannot create instance of {Type.FullName} via GetUninitializedObject.", e);
        }
    }
}