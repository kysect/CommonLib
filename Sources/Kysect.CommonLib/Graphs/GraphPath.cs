﻿namespace Kysect.CommonLib.Graphs;

public class GraphPath<T>
{
    public GraphPath(IReadOnlyCollection<T> elements)
    {
        Elements = elements;
    }

    public static GraphPath<T> Empty { get; } = new GraphPath<T>(Array.Empty<T>());

    public IReadOnlyCollection<T> Elements { get; }

    public GraphPath<T> AppendToStart(T value)
    {
        var result = new List<T>();
        result.Add(value);
        result.AddRange(Elements);
        return new GraphPath<T>(result);
    }
}