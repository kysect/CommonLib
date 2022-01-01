namespace Kysect.CommonLib.Paths;

public class PartialFilePath
{
    public FileInfo File { get; }
    public PartialPath FilePath { get; }
    public PartialPath ParentDirectoryPath { get; }

    public PartialFilePath(string root, string fileFullPath)
    {
        File = new FileInfo(fileFullPath);
        FilePath = new PartialPath(root, fileFullPath);
        ParentDirectoryPath = File.DirectoryName is null
            ? new PartialPath(string.Empty, string.Empty)
            : new PartialPath(root, File.DirectoryName);
    }

    public override bool Equals(object? obj)
    {
        return obj is PartialFilePath other
            && Equals(other);
    }

    public bool Equals(PartialFilePath other)
    {
        return FilePath.Equals(other.FilePath);
    }

    public override int GetHashCode()
    {
        return FilePath.GetHashCode();
    }

    public override string ToString()
    {
        return FilePath.ToString();
    }
}