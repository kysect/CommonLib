using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Kysect.CommonLib.DependencyInjection;

public static class ServiceCollectionConfigurationExtensions
{
    public static T GetRequired<T>(this IConfiguration configuration)
    {
        return configuration.Get<T>() ?? throw new ArgumentNullException(nameof(T));
    }

    public static IServiceCollection AddOptionsWithValidation<T>(this IServiceCollection serviceCollection, string name, bool validateOnStart = false) where T : class
    {
        OptionsBuilder<T> builder = serviceCollection
            .AddOptions<T>()
            .BindConfiguration(name)
            .ValidateDataAnnotations();

        if (validateOnStart)
            builder = builder.ValidateOnStart();

        return builder.Services;
    }
}