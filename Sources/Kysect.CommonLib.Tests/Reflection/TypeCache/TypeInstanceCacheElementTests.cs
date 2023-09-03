﻿using FluentAssertions;
using Kysect.CommonLib.Reflection;
using Kysect.CommonLib.Reflection.TypeCache;

namespace Kysect.CommonLib.Tests.Reflection.TypeCache;

public class TypeInstanceCacheElementTests
{
    [Test]
    public void CreateEmptyInstance_ForSimpleType_ReturnExpectedValue()
    {
        int emptyInstance = TypeInstanceCache<int>.CreateEmptyInstance();

        emptyInstance.Should().Be(0);
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