using Kysect.CommonLib.BaseTypes.Extensions;
using System.IO.Abstractions;

namespace Kysect.CommonLib.FileSystem;

public static class FileExtensions
{
    public static void EnsureFileExists(IFileSystem fileSystem, RelativePath path)
    {
        EnsureFileExists(fileSystem, path.Value);
    }

    public static void EnsureFileExists(IFileSystem fileSystem, string path)
    {
        fileSystem.ThrowIfNull();
        path.ThrowIfNull();

        if (fileSystem.File.Exists(path))
            return;

        DirectoryExtensions.EnsureParentDirectoryExists(fileSystem, path);

        using FileSystemStream stream = fileSystem.File.Create(path);
        stream.Close();
    }
}