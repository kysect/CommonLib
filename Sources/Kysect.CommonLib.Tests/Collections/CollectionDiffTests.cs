using FluentAssertions;
using Kysect.CommonLib.Collections.Diff;

namespace Kysect.CommonLib.Tests.Collections;

public class CollectionDiffTests
{
    private class TestContainer
    {
        public int Value { get; }
        public Guid RandomId { get; }

        public TestContainer(int value)
        {
            Value = value;
            RandomId = Guid.NewGuid();
        }
    }

    private class TestContainerComparator : IEqualityComparer<TestContainer>
    {
        public static TestContainerComparator Instance { get; } = new TestContainerComparator();

        public bool Equals(TestContainer? x, TestContainer? y)
        {
            if (ReferenceEquals(x, y))
                return true;
            if (x is null || y is null)
                return false;

            return x.Value == y.Value;
        }

        public int GetHashCode(TestContainer obj)
        {
            return obj.Value;
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
        var leftElement = new TestContainer(1);
        var rightElement = new TestContainer(1);

        var left = new List<TestContainer>()
        {
            leftElement,
        };

        var right = new List<TestContainer>()
        {
            rightElement,
        };

        var diff = CollectionDiff.Create(left, right, TestContainerComparator.Instance);

        diff.Added.Should().BeEmpty();
        diff.Removed.Should().BeEmpty();
        diff.Same.Should().Contain(leftElement);
    }
}