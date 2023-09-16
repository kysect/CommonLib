using Kysect.CommonLib.BaseTypes.Extensions;
using Kysect.CommonLib.Reflection.TypeCache;
using System.Reflection;

namespace Kysect.CommonLib.Reflection;

public static class AssemblyReflectionTraverser
{
    public static IReadOnlyCollection<Type> GetAllClasses(IReadOnlyCollection<Assembly> assemblies)
    {
        return assemblies
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => type.IsClass)
            .ToList();
    }

    public static IReadOnlyCollection<Type> GetAllImplementationOf<T>(IReadOnlyCollection<Assembly> assemblies)
    {
        return GetAllImplementationOf(assemblies, TypeInstanceCache<T>.Instance);
    }

    public static IReadOnlyCollection<Type> GetAllImplementationOf(IReadOnlyCollection<Assembly> assemblies, Type searchingType)
    {
        assemblies.ThrowIfNull();
        searchingType.ThrowIfNull();

        if (searchingType is { IsGenericType: true, IsGenericTypeDefinition: true })
        {
            return GetAllClasses(assemblies)
                .Where(t => FindInterfaceImplementationByGenericTypeDefinition(t, searchingType) is not null)
                .ToList();
        }

        var types = GetAllClasses(assemblies)
            .Where(searchingType.IsAssignableFrom)
            .ToList();

        return types;
    }

    public static Type? FindInterfaceImplementationByGenericTypeDefinition(Type sourceType, Type interfaceGenericTypeDefinition)
    {
        sourceType.ThrowIfNull();
        interfaceGenericTypeDefinition.ThrowIfNull();

        return sourceType
            .GetInterfaces()
            .Where(i => i.IsGenericType)
            .SingleOrDefault(i => i.GetGenericTypeDefinition() == interfaceGenericTypeDefinition);
    }
}