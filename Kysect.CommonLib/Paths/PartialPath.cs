namespace Kysect.CommonLib.Paths;

public class PartialPath
{
    public string Root { get; }
    public string Path { get; }

    public PartialPath(string root, string fullPath)
    {
        ArgumentNullException.ThrowIfNull(root);
        ArgumentNullException.ThrowIfNull(fullPath);

        if (fullPath.StartsWith(root))
            throw new ArgumentException("Full path should start with root path");

        Root = root;

        if (root == fullPath)
            Path = string.Empty;
        else if (string.IsNullOrWhiteSpace(root))
            Path = fullPath;
        else
        {
            Path = fullPath.Substring(root.Length);
            if (Path[0] == System.IO.Path.DirectorySeparatorChar)
                Path = Path.Remove(0, 1);
        }
    }

    public static PartialPath FromRoot(string fullPath) => new PartialPath(string.Empty, fullPath);

    public override bool Equals(object? obj)
    {
        return obj is PartialPath other && Equals(other);
    }

    public bool Equals(PartialPath other)
    {
        return Path.Equals(other.Path);
    }

    public override int GetHashCode()
    {
        return Path.GetHashCode();
    }

    public override string ToString()
    {
        return Path;
    }
}