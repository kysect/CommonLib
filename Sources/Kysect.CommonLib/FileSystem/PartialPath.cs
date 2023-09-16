namespace Kysect.CommonLib.FileSystem;

public readonly struct PartialPath : IEquatable<PartialPath>
{
    public string Root { get; }
    public string Value { get; }
    public string FullPath { get; }

    public PartialPath(string root, string fullPath)
    {
        if (root is null)
            throw new ArgumentNullException(nameof(root));
        if (fullPath is null)
            throw new ArgumentNullException(nameof(fullPath));

        if (!fullPath.StartsWith(root))
            throw new ArgumentException("Full path should start with root path");

        Root = root;
        FullPath = fullPath;

        if (root == fullPath)
        {
            Value = string.Empty;
        }
        else if (string.IsNullOrWhiteSpace(root))
        {
            Value = fullPath;
        }
        else
        {
            Value = fullPath.Substring(root.Length);
            if (Value[0] == Path.DirectorySeparatorChar)
                Value = Value.Remove(0, 1);
        }
    }

    public static PartialPath FromRoot(string fullPath)
    {
        return new PartialPath(string.Empty, fullPath);
    }

    public override bool Equals(object? obj)
    {
        return obj is PartialPath other && Equals(other);
    }

    public readonly bool Equals(PartialPath other)
    {
        return Value.Equals(other.Value, StringComparison.Ordinal);
    }

    public override readonly int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public override readonly string ToString()
    {
        return Value;
    }

    public static bool operator ==(PartialPath left, PartialPath right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(PartialPath left, PartialPath right)
    {
        return !(left == right);
    }
}