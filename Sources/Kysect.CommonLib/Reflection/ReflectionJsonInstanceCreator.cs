using Kysect.CommonLib.BaseTypes.Extensions;
using Kysect.CommonLib.Reflection.TypeCache;
using Newtonsoft.Json;

namespace Kysect.CommonLib.Reflection;

public class ReflectionJsonInstanceCreator
{
    public static T Create<T>(Dictionary<string, string> values)
    {
        return Create(TypeInstanceCache<T>.Instance, values).To<T>();
    }

    public static object Create(Type targetType, Dictionary<string, object> values)
    {
        string serializedDictionary = JsonConvert.SerializeObject(values);
        object? result = JsonConvert.DeserializeObject(serializedDictionary, targetType);
        if (result is null)
            throw new ArgumentException(nameof(result), $"Failed to convert dictionary to type {targetType.FullName}");

        return result;
    }

    public static object Create(Type targetType, Dictionary<string, string> values)
    {
        string serializedDictionary = JsonConvert.SerializeObject(values);
        object? result = JsonConvert.DeserializeObject(serializedDictionary, targetType);
        if (result is null)
            throw new ArgumentException(nameof(result), $"Failed to convert dictionary to type {targetType.FullName}");

        return result;
    }
}