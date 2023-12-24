using FluentAssertions;
using Kysect.CommonLib.Reflection;
using Kysect.CommonLib.Reflection.TypeCache;

namespace Kysect.CommonLib.Tests.Reflection.Searching;

public class ReflectionAttributeTests
{
    private readonly ReflectionAttributeFinder _reflectionAttributeFinder;

    public ReflectionAttributeTests()
    {
        _reflectionAttributeFinder = new ReflectionAttributeFinder();
    }

    [AttributeUsage(AttributeTargets.Enum)]
    public sealed class TestFakeAttribute<T> : Attribute
        where T : struct, Enum
    {
        public T Value { get; }

        public TestFakeAttribute(T value)
        {
            Value = value;
        }
    }

    [TestFake<DayOfWeek>(DayOfWeek.Monday)]
    public enum SomeTestType
    {
        Process = 1
    }

    [Test]
    public void AttributeChecker_ForGenericAttribute_ReturnAttribute()
    {
        Attribute? customAttribute = _reflectionAttributeFinder.GetAttributeFromType<TestFakeAttribute<DayOfWeek>>(TypeInstanceCache<SomeTestType>.Instance);

        customAttribute.Should().NotBeNull();
    }

    [Test]
    public void AttributeChecker_ForTypeWithoutAttribute_ReturnFalse()
    {
        bool hasAttribute = _reflectionAttributeFinder.HasAttribute<AuthorAttribute>(TypeInstanceCache<object>.Instance);

        hasAttribute.Should().BeFalse();
    }
}