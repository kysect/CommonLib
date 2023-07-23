using Kysect.CommonLib.Reflection;
using Kysect.CommonLib.Reflection.TypeCache;

namespace Kysect.CommonLib.Tests.Reflection.TypeCache;

public class TypeInstanceCacheElementTests
{
    [Test]
    public void CreateEmptyInstance_ForSimpleType_ReturnExpectedValue()
    {
        int emptyInstance = TypeInstanceCache<int>.CreateEmptyInstance();

        Assert.AreEqual(0, emptyInstance);
    }

    [Test]
    public void CreateEmptyInstance_ForInterface_ThrowException()
    {
        Assert.Throws<ReflectionException>(() =>
        {
            TypeInstanceCache<IDisposable>.CreateEmptyInstance();
        });
    }
}