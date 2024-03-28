using Kysect.CommonLib.BaseTypes.Extensions;
using System.IO.Abstractions;

namespace Kysect.CommonLib.FileSystem;

public static class DirectoryExtensions
{
    public static void EnsureParentDirectoryExists(this IFileSystem fileSystem, string path)
    {
        fileSystem.ThrowIfNull();
        path.ThrowIfNull();

        IFileInfo fileInfo = fileSystem.FileInfo.New(path);
        EnsureParentDirectoryExists(fileSystem, fileInfo);
    }

    public static void EnsureParentDirectoryExists(this IFileSystem fileSystem, IFileInfo file)
    {
        fileSystem.ThrowIfNull();
        file.ThrowIfNull();

        if (file.Directory is null)
            throw new ArgumentException($"File {file.FullName} does not have directory");

        EnsureDirectoryExists(fileSystem, file.Directory);
    }

    public static void EnsureDirectoryExists(this IFileSystem fileSystem, IDirectoryInfo directory)
    {
        fileSystem.ThrowIfNull();
        directory.ThrowIfNull();

        EnsureDirectoryExists(fileSystem, directory.FullName);
    }

    public static void EnsureDirectoryExists(this IFileSystem fileSystem, string path)
    {
        fileSystem.ThrowIfNull();
        path.ThrowIfNull();

        if (fileSystem.Directory.Exists(path))
            return;

        fileSystem.Directory.CreateDirectory(path);
    }
}