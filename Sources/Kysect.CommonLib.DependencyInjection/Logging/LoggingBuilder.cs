using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Kysect.CommonLib.DependencyInjection.Logging;

internal class LoggingBuilder : ILoggingBuilder
{
    public IServiceCollection Services { get; }

    public LoggingBuilder(IServiceCollection services)
    {
        Services = services;
    }
}