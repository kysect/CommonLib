using FluentAssertions;
using Kysect.CommonLib.Collections.Diff;

namespace Kysect.CommonLib.Tests.Collections;

public class CollectionDiffTests
{
    private class TestContainer
    {
        public int Value { get; }

        public TestContainer(int value)
        {
            Value = value;
        }

        public override bool Equals(object? obj)
        {
            return obj is TestContainer testContainer
                && testContainer.Value.Equals(Value);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value);
        }
    }

    [Test]
    public void Create_ForAdded_ReturnExpected()
    {
        var added = new TestContainer(1);

        var left = new List<TestContainer>()
        {
        };

        var right = new List<TestContainer>()
        {
            added,
        };

        var diff = CollectionDiff.Create(left, right);

        diff.Added.Should().Contain(added);
        diff.Removed.Should().BeEmpty();
        diff.Same.Should().BeEmpty();
    }

    [Test]
    public void Create_ForRemoved_ReturnExpected()
    {
        var removed = new TestContainer(1);

        var left = new List<TestContainer>()
        {
            removed,
        };

        var right = new List<TestContainer>()
        {
        };

        var diff = CollectionDiff.Create(left, right);

        diff.Added.Should().BeEmpty();
        diff.Removed.Should().Contain(removed);
        diff.Same.Should().BeEmpty();
    }

    [Test]
    public void Create_ForSame_ReturnExpected()
    {
        var element = new TestContainer(1);

        var left = new List<TestContainer>()
        {
            element,
        };

        var right = new List<TestContainer>()
        {
            element,
        };

        var diff = CollectionDiff.Create(left, right);

        diff.Added.Should().BeEmpty();
        diff.Removed.Should().BeEmpty();
        diff.Same.Should().Contain(element);
    }
}