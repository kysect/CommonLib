using Kysect.CommonLib.BaseTypes.Extensions;

namespace Kysect.CommonLib.Graphs;

public static class GraphBuilder
{
    public static GraphBuildResult<TKey, T> Build<TKey, T>(
        IReadOnlyCollection<TKey> nodes,
        IReadOnlyCollection<GraphLink<TKey>> links,
        GraphValueResolver<TKey, T> resolver)
        where TKey : notnull
    {
        nodes.ThrowIfNull();
        links.ThrowIfNull();
        resolver.ThrowIfNull();

        var targetNodes = new HashSet<TKey>(links.Select(l => l.To));

        var rootNodes = nodes
            .Where(l => !targetNodes.Contains(l))
            .ToList();

        ILookup<TKey, TKey> nodeLinks = links.ToLookup(l => l.From, l => l.To);

        var result = new List<GraphNode<TKey, T>>();

        foreach (TKey rootNode in rootNodes)
            result.Add(BuildNode(rootNode, nodeLinks, resolver));

        return new GraphBuildResult<TKey, T>(result);
    }

    private static GraphNode<TKey, T> BuildNode<TKey, T>(TKey id, ILookup<TKey, TKey> nodeLinks, IGraphValueResolver<TKey, T> resolver) where TKey : notnull
    {
        IReadOnlyCollection<GraphNode<TKey, T>> children = nodeLinks.Contains(id)
            ? BuildChildren(nodeLinks[id], nodeLinks, resolver)
            : Array.Empty<GraphNode<TKey, T>>();

        return new GraphNode<TKey, T>(id, resolver.Resolve(id), children);
    }

    private static IReadOnlyCollection<GraphNode<TKey, T>> BuildChildren<TKey, T>(
        IEnumerable<TKey> identifiers,
        ILookup<TKey, TKey> nodeLinks,
        IGraphValueResolver<TKey, T> resolver)
        where TKey : notnull
    {
        return identifiers
            .Select(childId => BuildNode(childId, nodeLinks, resolver))
            .ToList();
    }
}