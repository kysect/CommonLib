namespace Kysect.CommonLib.DateAndTime;

public readonly struct DateTimeInterval : IEquatable<DateTimeInterval>
{
    public DateTimeOffset Start { get; }
    public DateTimeOffset End { get; }

    public DateTimeInterval(DateTimeOffset start, DateTimeOffset end)
    {
        if (start > end)
            throw new ArgumentException("Interval end is lower that start.");

        Start = start;
        End = end;
    }

    public override bool Equals(object? obj)
    {
        return obj is DateTimeInterval interval && Equals(interval);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Start, End);
    }

    public static bool operator ==(DateTimeInterval left, DateTimeInterval right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(DateTimeInterval left, DateTimeInterval right)
    {
        return !(left == right);
    }

    public bool Equals(DateTimeInterval other)
    {
        return Start.Equals(other.Start) &&
               End.Equals(other.End);
    }
}