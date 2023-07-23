using Kysect.CommonLib.Reflection;

namespace Kysect.CommonLib.Tests.Reflection.InstanceModification;

public class ReflectionJsonInstanceCreatorTests
{
    [Test]
    public void InstanceCreator_CreateFromDictionary_ReturnExpectedValues()
    {
        var instance = new SomeType(1, "Name");
        var dictionary = new Dictionary<string, string>
        {
            ["Id"] = instance.Id.ToString(),
            ["Name"] = instance.Name
        };

        SomeType result = ReflectionJsonInstanceCreator.Create<SomeType>(dictionary);

        Assert.IsNotNull(result);
        Assert.That(result, Is.EqualTo(instance));
    }

    public record SomeType(int Id, string Name);
}