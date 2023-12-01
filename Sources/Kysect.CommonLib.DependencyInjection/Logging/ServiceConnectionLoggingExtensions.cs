using Kysect.CommonLib.BaseTypes.Extensions;
using Kysect.CommonLib.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Kysect.CommonLib.DependencyInjection.Logging;

public static class ServiceConnectionLoggingExtensions
{
    public static IServiceCollection AddLoggerConfiguration(this IServiceCollection serviceCollection, Action<LogConfigurationBuilder> configurationAction)
    {
        configurationAction.ThrowIfNull();

        using var logConfigurationBuilder = new LogConfigurationBuilder();
        configurationAction(logConfigurationBuilder);
        logConfigurationBuilder.Register(serviceCollection);
        return serviceCollection;
    }

    public static IServiceCollection AddTraceEventLogging(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddSingleton(s =>
            {
                ILoggerFactory factory = s.GetRequiredService<ILoggerFactory>();
                var listener = new LoggerTraceListener(factory);
                Trace.Listeners.Add(listener);
                return listener;
            });
    }
}