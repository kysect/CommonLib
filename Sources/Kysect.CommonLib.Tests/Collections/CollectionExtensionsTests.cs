using FluentAssertions;
using Kysect.CommonLib.Collections.Extensions;

namespace Kysect.CommonLib.Tests.Collections;

public class CollectionExtensionsTests
{
    [Fact]
    public void WhereNotNull_ForCollectionWithNullElement_ReturnNonNullCollection()
    {
        string?[] input = ["a", null, "b"];
        string[] expected = ["a", "b"];

        input.WhereNotNull().Should().BeEquivalentTo(expected);
    }
}