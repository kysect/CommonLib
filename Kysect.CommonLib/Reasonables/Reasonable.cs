namespace Kysect.CommonLib.Reasonables
{
    public static class Reasonable
    {
        public static Reasonable<T> Create<T>(T value) => new Reasonable<T>(value, null);
        public static Reasonable<T> Create<T>(T value, string reason) => new Reasonable<T>(value, reason);
    }

    public class Reasonable<T>
    {
        public T Value { get; }
        public string? Reason { get; }

        public Reasonable(T value, string? reason)
        {
            ArgumentNullException.ThrowIfNull(value);

            Value = value;
            Reason = reason;
        }

        public static implicit operator T(Reasonable<T> value)
        {
            return value.Value;
        }

        public string Format()
        {
            if (string.IsNullOrWhiteSpace(Reason))
                return Value.ToString();

            return $"{Value} ({Reason})";
        }

        public override string ToString()
        {
            return Format();
        }
    }
}