using FluentAssertions;
using Kysect.CommonLib.BaseTypes.Extensions;

namespace Kysect.CommonLib.Tests;

public class GenericExtensionsTests
{
    [Test]
    public void ThrowIfNull_ShouldThrowExceptionWithCorrectMessage()
    {
        int? myVariableName = null;

        ArgumentNullException? argumentNullException = Assert.Throws<ArgumentNullException>(() =>
        {
            myVariableName.ThrowIfNull();
        });

        argumentNullException.Should().NotBeNull();
        argumentNullException.Message.Should().Be($"Value cannot be null. (Parameter '{nameof(myVariableName)}')");
    }
}