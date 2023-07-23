using Kysect.CommonLib.Reflection;

namespace Kysect.CommonLib.Tests.Reflection;

public class ReflectionTraverserTests
{
    [Test]
    public void GetAllImplementationOf_ForInterface_ShouldReturnAllInheritors()
    {
        IReadOnlyCollection<Type> implementations = ReflectionTraverser.GetAllImplementationOf<IBaseInterface>(new[] { typeof(IBaseInterface).Assembly });

        Assert.That(typeof(IDerivedInterface).IsAssignableFrom(typeof(Implementation)));
        Assert.That(typeof(IBaseInterface).IsAssignableFrom(typeof(Implementation)));
        Assert.That(implementations.Count, Is.EqualTo(1));
        Assert.That(implementations, Contains.Item(typeof(Implementation)));
    }

    private interface IBaseInterface
    {
    }

    private interface IDerivedInterface : IBaseInterface
    {
    }

    private class Implementation : IDerivedInterface
    {
    }
}