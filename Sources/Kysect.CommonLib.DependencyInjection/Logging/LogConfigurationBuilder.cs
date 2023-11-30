using Lunet.Extensions.Logging.SpectreConsole;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Extensions.Logging;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Kysect.CommonLib.DependencyInjection.Logging;

public sealed class LogConfigurationBuilder : IDisposable
{
    private readonly Stack<IDisposable> _disposables = new Stack<IDisposable>();
    private bool _disposedValue;
    private readonly ServiceCollection _serviceCollection;
    private readonly ILoggingBuilder _loggingBuilder;

    private string _rootDirectoryPath = string.Empty;
    private string _defaultCategory = string.Empty;
    private LogLevel _logLevel = LogLevel.Information;

    public LogConfigurationBuilder()
    {
        _serviceCollection = new ServiceCollection();

        _serviceCollection.AddSingleton<ILoggerFactory, LoggerFactory>();
        _serviceCollection.AddSingleton(typeof(ILogger<>), typeof(Logger<>));

        _loggingBuilder = new LoggingBuilder(_serviceCollection);
    }

    public LogConfigurationBuilder SetLevel(LogLevel level)
    {
        _logLevel = level;
        _loggingBuilder.AddFilter(null, _logLevel);
        return this;
    }

    public LogConfigurationBuilder SetRedirectToAppData(params string[] parts)
    {
        string pathToAppData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
        _rootDirectoryPath = Path.Combine(parts.Prepend(pathToAppData).ToArray());
        return this;
    }

    public LogConfigurationBuilder SetDefaultCategory(string defaultCategory)
    {
        _defaultCategory = defaultCategory;
        return this;
    }

    public LogConfigurationBuilder AddDefaultConsole()
    {
        _loggingBuilder.AddSimpleConsole(options =>
        {
            options.IncludeScopes = false;
            options.SingleLine = true;
            options.TimestampFormat = "HH:mm:ss";
        });
        return this;
    }

    public LogConfigurationBuilder AddSpectreConsole()
    {
        var spectreConsoleLoggerOptions = new SpectreConsoleLoggerOptions()
        {
            IncludeNewLineBeforeMessage = false,
            IncludeTimestamp = true,
            IncludeLogLevel = false,
            IncludeCategory = false,
            LogException = true,
            TimestampFormat = "HH:mm:ss"
        };

#pragma warning disable CA2000 // Dispose objects before losing scope
        var spectreConsoleLoggerProvider = new SpectreConsoleLoggerProvider(spectreConsoleLoggerOptions);
#pragma warning restore CA2000 // Dispose objects before losing scope
        _disposables.Push(spectreConsoleLoggerProvider);
        _loggingBuilder.AddProvider(spectreConsoleLoggerProvider);

        return this;
    }

    public LogConfigurationBuilder AddSerilogToFile(string fileName)
    {
        string fullPath = Path.Combine(_rootDirectoryPath, fileName);
        LoggerConfiguration loggerConfiguration = new LoggerConfiguration()
            .WriteTo.File(path: fullPath, rollingInterval: RollingInterval.Day);

#pragma warning disable CA2000 // Dispose objects before losing scope
        var serilogLoggerProvider = new SerilogLoggerProvider(loggerConfiguration.CreateLogger());
#pragma warning restore CA2000 // Dispose objects before losing scope
        _disposables.Push(serilogLoggerProvider);
        _loggingBuilder.AddProvider(serilogLoggerProvider);

        return this;
    }

    public void Register(IServiceCollection serviceCollection)
    {
        ServiceProvider serviceProvider = _serviceCollection.BuildServiceProvider();
        serviceCollection.AddSingleton(serviceProvider.GetRequiredService<ILoggerFactory>());
        serviceCollection.AddSingleton(serviceProvider.GetRequiredService<ILogger>());
    }

    public ILogger Build()
    {
        ServiceProvider serviceProvider = _serviceCollection.BuildServiceProvider();
        ILoggerFactory loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
        return loggerFactory.CreateLogger(_defaultCategory);
    }

    private void Dispose(bool disposing)
    {
        if (_disposedValue)
            return;

        if (disposing)
        {
            while (_disposables.Count > 0)
            {
                _disposables.Pop().Dispose();
            }
        }

        _disposedValue = true;
    }

    public void Dispose()
    {
        Dispose(disposing: true);
    }
}