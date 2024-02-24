using FluentAssertions;
using Kysect.CommonLib.Logging;
using Kysect.CommonLib.ProgressTracking;
using Microsoft.Extensions.Logging;

namespace Kysect.CommonLib.Tests.ProgressTracking;

public class LoggerProgressTrackerTests
{
    private LoggerProgressTrackerFactory _loggerProgressTrackerFactory;
    private StringBuilderLogger<LoggerProgressTrackerTests> _stringBuilderLogger;

    [SetUp]
    public void Setup()
    {
        var logLevel = LogLevel.Trace;
        _stringBuilderLogger = new StringBuilderLogger<LoggerProgressTrackerTests>(logLevel);
        _loggerProgressTrackerFactory = new LoggerProgressTrackerFactory(_stringBuilderLogger, logLevel);
    }

    [Test]
    public void OnUpdate_ThreeCalls_GenerateThreeLogLine()
    {
        IProgressTracker progressTracker = _loggerProgressTrackerFactory.Create("Operation name", 3);

        progressTracker.OnUpdate();
        _stringBuilderLogger.Build().Should().BeEquivalentTo(["[Operation name] Processed 1/3"]);

        progressTracker.OnUpdate();
        _stringBuilderLogger.Build().Should().BeEquivalentTo(["[Operation name] Processed 1/3", "[Operation name] Processed 2/3"]);

        progressTracker.OnUpdate();
        _stringBuilderLogger.Build().Should().BeEquivalentTo(["[Operation name] Processed 1/3", "[Operation name] Processed 2/3", "[Operation name] Processed 3/3"]);
    }
}