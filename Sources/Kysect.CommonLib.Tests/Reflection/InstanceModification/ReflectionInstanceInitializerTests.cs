using FluentAssertions;
using Kysect.CommonLib.Reflection;

namespace Kysect.CommonLib.Tests.Reflection.InstanceModification;

public class ReflectionInstanceInitializerTests
{
    public record TypeForInitialized(string Value1, int Value2);

    [Test]
    public void ReflectionInstanceInitializer_ForInitializedInstance_ReturnExpectedValues()
    {
        var expected = new TypeForInitialized("Value", 1);
        var reflectionInstanceInitializer = new ReflectionInstanceInitializer<TypeForInitialized>();

        TypeForInitialized actual = reflectionInstanceInitializer.GetValue();
        bool firstFieldInitialized = reflectionInstanceInitializer.Set(nameof(TypeForInitialized.Value1), expected.Value1);
        bool secondFieldInitialized = reflectionInstanceInitializer.Set(nameof(TypeForInitialized.Value2), expected.Value2);

        firstFieldInitialized.Should().BeTrue();
        secondFieldInitialized.Should().BeTrue();
        actual.Should().Be(expected);
    }
}