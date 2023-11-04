using FluentAssertions;
using Kysect.CommonLib.BaseTypes;

namespace Kysect.CommonLib.Tests;

public class EnumStringValueTests
{
    public enum TestType
    {
        [EnumStringValue("First value")]
        FirstValue,

        [EnumStringValue("2 value")]
        SecondValue,
    }

    [Test]
    [TestCase(TestType.FirstValue, "First value")]
    [TestCase(TestType.SecondValue, "2 value")]
    public void ToEnumStringAndBack_ShouldReturnCorrectValue(TestType enumValue, string stringValue)
    {
        string actualStringValue = EnumStringValue.ToEnumString(enumValue);
        TestType actualEnumValue = EnumStringValue.FromEnumString<TestType>(stringValue);

        actualStringValue.Should().Be(stringValue);
        actualEnumValue.Should().Be(enumValue);
    }
}