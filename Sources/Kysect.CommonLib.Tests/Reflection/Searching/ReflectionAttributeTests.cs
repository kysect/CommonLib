using Kysect.CommonLib.Reflection;
using Kysect.CommonLib.Reflection.TypeCache;

namespace Kysect.CommonLib.Tests.Reflection.Searching;

public class ReflectionAttributeTests
{
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
        Attribute? customAttribute = TypeInstanceCache<SomeTestType>.Instance.GetAttribute<TestFakeAttribute<DayOfWeek>>();

        Assert.NotNull(customAttribute);
    }

    [Test]
    public void AttributeChecker_ForTypeWithoutAttribute_ReturnFalse()
    {
        bool hasAttribute = TypeInstanceCache<object>.Instance.HasAttribute<AuthorAttribute>();

        Assert.IsFalse(hasAttribute);
    }
}