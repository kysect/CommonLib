using Kysect.CommonLib.Paths;

namespace Kysect.CommonLib;

public static class FileExtensions
{
    public static void EnsureFileExists(PartialPath path) => EnsureFileExists(path.FullPath);

    public static void EnsureFileExists(string path)
    {
        if (path is null)
            throw new ArgumentNullException(nameof(path));

        if (File.Exists(path)) 
            return;

        DirectoryInfo parent = Directory.GetParent(path) ?? throw new Exception($"Unexpected error while trying to get path to cache folder. Cache path: {path}");
        Directory.CreateDirectory(parent.FullName);
        using FileStream stream = File.Create(path);
        stream.Close();
    }
}