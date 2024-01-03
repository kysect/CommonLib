namespace Kysect.CommonLib.Graphs;

public class GraphBuildResult<TKey, T> where TKey : notnull
{
    public GraphBuildResult(IReadOnlyCollection<GraphNode<TKey, T>> roots)
    {
        Roots = roots;
    }

    public IReadOnlyCollection<GraphNode<TKey, T>> Roots { get; }

    public GraphNode<TKey, T> GetValue(TKey id)
    {
        GraphNode<TKey, T>? found = Roots
            .Select(root => root.Find(id))
            .FirstOrDefault(value => value is not null);

        return found ?? throw new ArgumentException($"Work item with id {id} was not found");
    }
}