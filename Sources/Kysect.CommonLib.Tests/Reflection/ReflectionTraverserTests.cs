using FluentAssertions;
using Kysect.CommonLib.Reflection;

namespace Kysect.CommonLib.Tests.Reflection;

public class ReflectionTraverserTests
{
    [Fact]
    public void GetAllImplementationOf_ForInterface_ShouldReturnAllInheritors()
    {
        IReadOnlyCollection<Type> implementations = AssemblyReflectionTraverser.GetAllImplementationOf<IBaseInterface>(new[] { typeof(IBaseInterface).Assembly });

        typeof(Implementation).Should().BeAssignableTo<IDerivedInterface>();
        typeof(Implementation).Should().BeAssignableTo<IBaseInterface>();
        implementations.Count.Should().Be(1);
        implementations.Should().Contain(typeof(Implementation));
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