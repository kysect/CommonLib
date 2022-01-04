namespace Kysect.CommonLib.Reasons
{
    public static class Reason
    {
        public static Reason<T> Create<T>(T value) => new Reason<T>(value, null);
        public static Reason<T> Create<T>(T value, string? reason) => new Reason<T>(value, reason);
    }

    public class Reason<T>
    {
        public T Value { get; }
        public string? Description { get; }

        public Reason(T value, string? description)
        {
            ArgumentNullException.ThrowIfNull(value);

            Value = value;
            Description = description;
        }

        public static implicit operator T(Reason<T> value)
        {
            return value.Value;
        }

        public string? Format()
        {
            ArgumentNullException.ThrowIfNull(Value);

            if (string.IsNullOrWhiteSpace(Description))
                return Value.ToString();

            return $"{Value} ({Description})";
        }

        public override string? ToString()
        {
            return Format();
        }
    }
}
