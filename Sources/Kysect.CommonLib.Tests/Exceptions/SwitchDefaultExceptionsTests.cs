using FluentAssertions;
using Kysect.CommonLib.BaseTypes.Extensions;
using Kysect.CommonLib.Exceptions;

namespace Kysect.CommonLib.Tests.Exceptions;

public class SwitchDefaultExceptionsTests
{
    public enum Sample
    {
        Value1
    }

    [Theory]
    [InlineData("Some String")]
    [InlineData(Sample.Value1)]
    [InlineData(123)]
    [InlineData(null)]
    public void OnUnexpectedValue_WithSpecifiedArgumentName_ReturnCorrectException(object? value)
    {
        string message = $"Value {value} is not expected (Parameter 'argumentName')";

        ArgumentOutOfRangeException actual = SwitchDefaultExceptions.OnUnexpectedValue(value, "argumentName");

        actual.Message.Should().Be(message);
    }

    [Theory]
    [InlineData("Some String")]
    [InlineData(Sample.Value1)]
    [InlineData(123)]
    [InlineData(null)]
    public void OnUnexpectedValue_WithoutSpecifiedArgumentName_ReturnCorrectException(object? value)
    {
        string message = $"Value {value} is not expected (Parameter 'value')";

        ArgumentOutOfRangeException actual = SwitchDefaultExceptions.OnUnexpectedValue(value);

        actual.Message.Should().Be(message);
    }

    [Theory]
    [InlineData("Some String")]
    [InlineData(Sample.Value1)]
    [InlineData(123)]
    public void OnUnexpectedType_WithoutSpecifiedArgumentName_ReturnCorrectException(object value)
    {
        value.ThrowIfNull();

        string message = $"Type {value.GetType().FullName} is not expected (Parameter 'value')";

        ArgumentOutOfRangeException actual = SwitchDefaultExceptions.OnUnexpectedType(value);

        actual.Message.Should().Be(message);
    }

    [Theory]
    [InlineData(null)]
    public void OnUnexpectedType_ForNull_ReturnCorrectException(object? value)
    {
        string message = $"Null value is not expected (Parameter 'value')";

        ArgumentOutOfRangeException actual = SwitchDefaultExceptions.OnUnexpectedType(value);

        actual.Message.Should().Be(message);
    }
}