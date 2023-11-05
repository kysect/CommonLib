using FluentAssertions;
using Kysect.CommonLib.BaseTypes.Extensions;
using Kysect.CommonLib.Exceptions;

namespace Kysect.CommonLib.Tests;

public class SwitchDefaultExceptionsTests
{
    public enum Sample
    {
        Value1
    }

    [Test]
    [TestCase("Some String")]
    [TestCase(Sample.Value1)]
    [TestCase(123)]
    [TestCase(null)]
    public void OnUnexpectedValue_WithSpecifiedArgumentName_ReturnCorrectException(object? value)
    {
        string message = $"Value {value} is not expected (Parameter 'argumentName')";

        ArgumentOutOfRangeException actual = SwitchDefaultExceptions.OnUnexpectedValue(value, "argumentName");

        actual.Message.Should().Be(message);
    }

    [Test]
    [TestCase("Some String")]
    [TestCase(Sample.Value1)]
    [TestCase(123)]
    [TestCase(null)]
    public void OnUnexpectedValue_WithoutSpecifiedArgumentName_ReturnCorrectException(object? value)
    {
        string message = $"Value {value} is not expected (Parameter 'value')";

        ArgumentOutOfRangeException actual = SwitchDefaultExceptions.OnUnexpectedValue(value);

        actual.Message.Should().Be(message);
    }

    [Test]
    [TestCase("Some String")]
    [TestCase(Sample.Value1)]
    [TestCase(123)]
    public void OnUnexpectedType_WithoutSpecifiedArgumentName_ReturnCorrectException(object value)
    {
        value.ThrowIfNull();

        string message = $"Type {value.GetType().FullName} is not expected (Parameter 'value')";

        ArgumentOutOfRangeException actual = SwitchDefaultExceptions.OnUnexpectedType(value);

        actual.Message.Should().Be(message);
    }

    [Test]
    [TestCase(null)]
    public void OnUnexpectedType_ForNull_ReturnCorrectException(object? value)
    {
        string message = $"Null value is not expected (Parameter 'value')";

        ArgumentOutOfRangeException actual = SwitchDefaultExceptions.OnUnexpectedType(value);

        actual.Message.Should().Be(message);
    }
}