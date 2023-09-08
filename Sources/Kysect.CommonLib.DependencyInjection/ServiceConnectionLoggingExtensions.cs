using Kysect.CommonLib.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Kysect.CommonLib.DependencyInjection;

public static class ServiceConnectionLoggingExtensions
{
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