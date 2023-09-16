using Kysect.CommonLib.BaseTypes.Extensions;
using Kysect.CommonLib.Reflection.TypeCache;
using System.Reflection;

namespace Kysect.CommonLib.Reflection;

public static class ReflectionInstanceInitializingExtensions
{
    public static bool TrySetToProperty(object instance, PropertyInfo? propertyInfo, object? newValue)
    {
        instance.ThrowIfNull();


        if (propertyInfo is null)
            return false;

        if (propertyInfo.CanWrite)
        {
            propertyInfo.SetValue(instance, newValue);
            return true;
        }

        FieldInfo? mappedField = TypeInstanceCache.GetOrAdd(instance.GetType()).FindPropertyBackingFieldName(propertyInfo.Name);

        mappedField.ThrowIfNull(nameof(mappedField));

        mappedField.SetValue(instance, newValue);
        return true;
    }
}