using System.Diagnostics.Contracts;

namespace Kysect.CommonLib.Graphs;

public record struct GraphLink<TKey>(TKey From, TKey To)
{
    [Pure]
    public readonly GraphLink<TKey> Reversed()
    {
        return new GraphLink<TKey>(From: To, To: From);
    }
}