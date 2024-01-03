namespace Kysect.CommonLib.Graphs;

public class GraphNode<TKey, T> where TKey : notnull
{
    public TKey Id { get; }
    public T Value { get; }
    public IReadOnlyCollection<GraphNode<TKey, T>> DirectChildren { get; }

    public GraphNode(TKey id, T value, IReadOnlyCollection<GraphNode<TKey, T>> directChildren)
    {
        Id = id;
        Value = value;
        DirectChildren = directChildren;
    }

    public IEnumerable<GraphNode<TKey, T>> EnumerateChildren()
    {
        if (!DirectChildren.Any())
            return Array.Empty<GraphNode<TKey, T>>();

        return DirectChildren
            .Concat(DirectChildren.SelectMany(c => c.EnumerateChildren()))
            .ToList();
    }

    public GraphNode<TKey, T>? Find(TKey id)
    {
        if (Id.Equals(id))
            return this;

        return DirectChildren
            .Select(node => node.Find(id))
            .FirstOrDefault(founded => founded is not null);
    }

    public override string ToString()
    {
        return $"Node {Value}, Direct children count: {DirectChildren.Count}";
    }
}