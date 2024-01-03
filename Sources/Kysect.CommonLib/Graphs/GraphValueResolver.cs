namespace Kysect.CommonLib.Graphs;

public class GraphValueResolver<TKey, T> : IGraphValueResolver<TKey, T>
{
    private readonly Dictionary<TKey, T> _map;

    public GraphValueResolver(IReadOnlyCollection<T> values, Func<T, TKey> selector)
    {
        _map = values.ToDictionary(selector, v => v);
    }

    public T Resolve(TKey id)
    {
        if (_map.TryGetValue(id, out T? result))
            return result;

        throw new ArgumentException($"Graph node with id {id} was not found");
    }
}