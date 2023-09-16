using Kysect.CommonLib.BaseTypes.Extensions;

namespace Kysect.CommonLib.Collections.Diff;

public static class CollectionDiff
{
    public static CollectionDiff<T> Create<T>(IReadOnlyCollection<T> left, IReadOnlyCollection<T> right)
    {
        return Create(left, right, EqualityComparer<T>.Default);
    }

    public static CollectionDiff<T> Create<T>(IReadOnlyCollection<T> left, IReadOnlyCollection<T> right, IEqualityComparer<T> comparer)
    {
        left.ThrowIfNull();
        right.ThrowIfNull();
        comparer.ThrowIfNull();

        var added = new List<T>();
        var removed = new List<T>();
        var same = new List<T>();

        foreach (T l in left)
        {
            if (right.Contains(l, comparer))
                same.Add(l);
            else
                removed.Add(l);
        }

        foreach (T r in right)
        {
            if (!left.Contains(r, comparer))
                added.Add(r);
        }

        return new CollectionDiff<T>(added, removed, same);
    }
}

public class CollectionDiff<T>
{
    public IReadOnlyCollection<T> Added { get; private set; }
    public IReadOnlyCollection<T> Removed { get; private set; }
    public IReadOnlyCollection<T> Same { get; private set; }

    public CollectionDiff(IReadOnlyCollection<T> added, IReadOnlyCollection<T> removed, IReadOnlyCollection<T> same)
    {
        Added = added;
        Removed = removed;
        Same = same;
    }
}