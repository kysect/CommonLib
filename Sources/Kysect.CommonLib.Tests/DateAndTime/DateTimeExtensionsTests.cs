using FluentAssertions;
using Kysect.CommonLib.DateAndTime;

namespace Kysect.CommonLib.Tests.DateAndTime;

public class DateTimeExtensionsTests
{
    [Test]
    public void ToOrderedShortString_ReturnExpectedResult()
    {
        var value = new DateTime(2023, 1, 2, 3, 4, 5);
        string expected = "2023-01-02";

        string result = value.ToOrderedShortString();

        result.Should().Be(expected);
    }

    [Test]
    public void ToOrderedFullString_ReturnExpectedResult()
    {
        var value = new DateTime(2023, 1, 2, 3, 4, 5);
        string expected = "2023-01-02-03-04-05";

        string result = value.ToOrderedFullString();

        result.Should().Be(expected);
    }
}