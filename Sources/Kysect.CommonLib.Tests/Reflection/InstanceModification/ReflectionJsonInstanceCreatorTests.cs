using Kysect.CommonLib.Reflection;

namespace Kysect.CommonLib.Tests.Reflection.InstanceModification;

public class ReflectionJsonInstanceCreatorTests
{
    private readonly ReflectionJsonInstanceCreator _instanceCreator;

    public ReflectionJsonInstanceCreatorTests()
    {
        _instanceCreator = ReflectionJsonInstanceCreator.Create();
    }

    [Test]
    public void InstanceCreator_CreateFromDictionary_ReturnExpectedValues()
    {
        var instance = new SomeType(1, "Name");
        var dictionary = new Dictionary<string, string>
        {
            ["Id"] = instance.Id.ToString(),
            ["Name"] = instance.Name
        };

        SomeType result = _instanceCreator.Create<SomeType>(dictionary);

        Assert.IsNotNull(result);
        Assert.That(result, Is.EqualTo(instance));
    }

    public record SomeType(int Id, string Name);
}