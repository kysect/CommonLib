using Kysect.CommonLib.BaseTypes.Extensions;
using Kysect.CommonLib.Reflection.TypeCache;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Kysect.CommonLib.Reflection;

public class ReflectionJsonInstanceCreator
{
    private readonly JsonSerializerOptions _options;

    public static ReflectionJsonInstanceCreator Create()
    {
        var options = new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowReadingFromString,
        };

        return new ReflectionJsonInstanceCreator(options);
    }

    public ReflectionJsonInstanceCreator(JsonSerializerOptions options)
    {
        _options = options.ThrowIfNull();
    }

    public T Create<T>(Dictionary<string, string> values)
    {
        return Create(TypeInstanceCache<T>.Instance, values).To<T>();
    }

    public object Create(Type targetType, Dictionary<string, object> values)
    {
        targetType.ThrowIfNull();
        values.ThrowIfNull();

        string serializedDictionary = JsonSerializer.Serialize(values, _options);
        object? result = JsonSerializer.Deserialize(serializedDictionary, targetType, _options);

        if (result is null)
            throw new ArgumentException(nameof(result), $"Failed to convert dictionary to type {targetType.FullName}");

        return result;
    }

    public object Create(Type targetType, Dictionary<string, string> values)
    {
        targetType.ThrowIfNull();
        values.ThrowIfNull();

        string serializedDictionary = JsonSerializer.Serialize(values, _options);
        object? result = JsonSerializer.Deserialize(serializedDictionary, targetType, _options);

        if (result is null)
            throw new ArgumentException(nameof(result), $"Failed to convert dictionary to type {targetType.FullName}");

        return result;
    }
}