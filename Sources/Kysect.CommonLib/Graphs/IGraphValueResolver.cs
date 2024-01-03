namespace Kysect.CommonLib.Graphs;

public interface IGraphValueResolver<TKey, T>
{
    T Resolve(TKey id);
}