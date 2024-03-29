﻿namespace Kysect.CommonLib.BaseTypes;

public static class Reason
{
    public static Reason<T> Create<T>(T value)
    {
        return new Reason<T>(value, null);
    }

    public static Reason<T> Create<T>(T value, string? reason)
    {
        return new Reason<T>(value, reason);
    }
}

public class Reason<T>
{
    public T? Value { get; }
    public string? Description { get; }

    public Reason(T? value, string? description)
    {
        Value = value;
        Description = description;
    }

    public static implicit operator T?(Reason<T>? value)
    {
        return value is null ? default : value.Value;
    }

    public string Format()
    {
        if (string.IsNullOrWhiteSpace(Description))
            return Value?.ToString() ?? "null";

        return $"{Value} ({Description})";
    }

    public override string? ToString()
    {
        return Format();
    }
}