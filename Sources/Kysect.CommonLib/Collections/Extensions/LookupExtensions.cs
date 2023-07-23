using System.Collections;

namespace Kysect.CommonLib.Collections.Extensions;

public static class LookupExtensions
{
    public class EmptyLookup<TKey, TValue> : ILookup<TKey, TValue>
    {
        public IEnumerator<IGrouping<TKey, TValue>> GetEnumerator()
        {
            return Enumerable.Empty<IGrouping<TKey, TValue>>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool Contains(TKey key)
        {
            return false;
        }

        public int Count => 0;

        public IEnumerable<TValue> this[TKey key] => throw new KeyNotFoundException(key?.ToString());
    }

    public static ILookup<TKey, TValue> ToLookup<TKey, TValue>(this Dictionary<TKey, List<TValue>> dictionary)
    {
        return dictionary
            .SelectMany(p => p.Value.Select(v => (p.Key, Value: v)))
            .ToLookup(p => p.Key, p => p.Value);
    }

    public static ILookup<TKey, TValue> Empty<TKey, TValue>()
    {
        return new EmptyLookup<TKey, TValue>();
    }
}
