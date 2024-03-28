using FluentAssertions;
using Kysect.CommonLib.Logging;
using Microsoft.Extensions.Logging;

namespace Kysect.CommonLib.Tests.Logging;

public class LoggerTabExtensionTests
{
    private readonly StringBuilderLogger<LoggerTabExtensionTests> _logger;

    public LoggerTabExtensionTests()
    {
        _logger = new StringBuilderLogger<LoggerTabExtensionTests>(LogLevel.Trace);
    }

    [Fact]
    public void LogTabTrace_ReturnExpectedString()
    {
        _logger.LogTabTrace(1, "Message");

        _logger.Build().Should().BeEquivalentTo(["\tMessage"]);
    }

    [Fact]
    public void LogTabDebug_ReturnExpectedString()
    {
        _logger.LogTabDebug(1, "Message");

        _logger.Build().Should().BeEquivalentTo(["\tMessage"]);
    }

    [Fact]
    public void LogTabInformation_ReturnExpectedString()
    {
        _logger.LogTabInformation(1, "Message");

        _logger.Build().Should().BeEquivalentTo(["\tMessage"]);
    }

    [Fact]
    public void LogTabWarning_ReturnExpectedString()
    {
        _logger.LogTabWarning(1, "Message");

        _logger.Build().Should().BeEquivalentTo(["\tMessage"]);
    }

    [Fact]
    public void LogTabError_ReturnExpectedString()
    {
        _logger.LogTabError(1, "Message");

        _logger.Build().Should().BeEquivalentTo(["\tMessage"]);
    }

    [Fact]
    public void LogTabCritical_ReturnExpectedString()
    {
        _logger.LogTabCritical(1, "Message");

        _logger.Build().Should().BeEquivalentTo(["\tMessage"]);
    }
}