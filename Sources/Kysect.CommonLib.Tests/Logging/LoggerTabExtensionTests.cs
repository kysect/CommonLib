using FluentAssertions;
using Kysect.CommonLib.Logging;
using Microsoft.Extensions.Logging;

namespace Kysect.CommonLib.Tests.Logging;

public class LoggerTabExtensionTests
{
    private StringBuilderLogger _logger;

    [SetUp]
    public void Setup()
    {
        _logger = new StringBuilderLogger(LogLevel.Trace);
    }

    [Test]
    public void LogTabTrace_ReturnExpectedString()
    {
        _logger.LogTabTrace(1, "Message");

        _logger.Build().Should().BeEquivalentTo(["\tMessage"]);
    }

    [Test]
    public void LogTabDebug_ReturnExpectedString()
    {
        _logger.LogTabDebug(1, "Message");

        _logger.Build().Should().BeEquivalentTo(["\tMessage"]);
    }

    [Test]
    public void LogTabInformation_ReturnExpectedString()
    {
        _logger.LogTabInformation(1, "Message");

        _logger.Build().Should().BeEquivalentTo(["\tMessage"]);
    }

    [Test]
    public void LogTabWarning_ReturnExpectedString()
    {
        _logger.LogTabWarning(1, "Message");

        _logger.Build().Should().BeEquivalentTo(["\tMessage"]);
    }

    [Test]
    public void LogTabError_ReturnExpectedString()
    {
        _logger.LogTabError(1, "Message");

        _logger.Build().Should().BeEquivalentTo(["\tMessage"]);
    }

    [Test]
    public void LogTabCritical_ReturnExpectedString()
    {
        _logger.LogTabCritical(1, "Message");

        _logger.Build().Should().BeEquivalentTo(["\tMessage"]);
    }
}