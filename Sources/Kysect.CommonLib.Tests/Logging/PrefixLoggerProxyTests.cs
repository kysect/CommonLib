using FluentAssertions;
using Kysect.CommonLib.Logging;
using Microsoft.Extensions.Logging;

namespace Kysect.CommonLib.Tests.Logging;

public class PrefixLoggerProxyTests
{
    private readonly StringBuilderLogger<PrefixLoggerProxyTests> _logger;

    public PrefixLoggerProxyTests()
    {
        _logger = new StringBuilderLogger<PrefixLoggerProxyTests>(LogLevel.Trace);
    }

    [Fact]
    public void WithPrefix_WriteString_ReturnStringWithPrefix()
    {
        ILogger logger = _logger.WithPrefix("Prefix");

        logger.LogInformation("Message");

        IReadOnlyCollection<string> loggedLines = _logger.Build();
        loggedLines.Should().HaveCount(1)
            .And.Subject.Single().Should().Be("[Prefix] Message");
    }

    [Fact]
    public void WithPrefixGeneric_WriteString_ReturnStringWithPrefix()
    {
        ILogger logger = _logger.WithPrefix<PrefixLoggerProxyTests>();

        logger.LogInformation("Message");

        IReadOnlyCollection<string> loggedLines = _logger.Build();
        loggedLines.Should().HaveCount(1)
            .And.Subject.Single().Should().Be("[PrefixLoggerProxyTests] Message");
    }
}