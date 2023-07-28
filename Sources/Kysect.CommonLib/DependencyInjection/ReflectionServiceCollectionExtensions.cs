using Kysect.CommonLib.Collections.Extensions;
using Kysect.CommonLib.Reflection;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Kysect.CommonLib.DependencyInjection;

public static class ReflectionServiceCollectionExtensions
{
    public static IServiceCollection AddAllImplementationOf<T>(this IServiceCollection services, params Assembly[] assemblies)
    {
        if (assemblies.IsEmpty())
            throw new ArgumentException("No assemblies was specified.");

        IReadOnlyCollection<Type> allImplementationOf = AssemblyReflectionTraverser.GetAllImplementationOf<T>(assemblies);

        foreach (Type type in allImplementationOf)
            services.AddSingleton(type);

        return services;
    }

    public static IServiceCollection AddRegisteredDependencyDisposer(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddSingleton<IocContainerDependencyDisposer>();
    }

    public static IServiceCollection AddSingletonWithDisposing<T>(this IServiceCollection serviceCollection, Func<IServiceProvider, T> provider) where T : class, IDisposable
    {
        return serviceCollection.AddSingleton(s =>
        {
            T value = provider(s);
            IocContainerDependencyDisposer disposer = s.GetRequiredService<IocContainerDependencyDisposer>();
            return disposer.Add(value);
        });
    }
}