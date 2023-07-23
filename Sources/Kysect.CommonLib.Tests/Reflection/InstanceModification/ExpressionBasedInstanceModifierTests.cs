using Kysect.CommonLib.Reflection;

namespace Kysect.CommonLib.Tests.Reflection.InstanceModification;

public class ExpressionBasedInstanceModifierTests
{
    public class TestType
    {
        public int Value { get; }

        public TestType(int value)
        {
            Value = value;
        }
    }

    [Test]
    public void ModifyProperty_WithValue_SetValueToProperty()
    {
        var expressionBasedInstanceModifier = new ExpressionBasedInstanceModifier();
        var testType = new TestType(0);
        const int newValue = 1;

        expressionBasedInstanceModifier.ModifyProperty(testType, i => i.Value, newValue);

        Assert.That(testType.Value, Is.EqualTo(newValue));
    }
}