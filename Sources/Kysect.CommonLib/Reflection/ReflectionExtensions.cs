using Kysect.CommonLib.BaseTypes.Extensions;

namespace Kysect.CommonLib.Reflection;

public static class ReflectionExtensions
{
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