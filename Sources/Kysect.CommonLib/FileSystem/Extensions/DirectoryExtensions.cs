using Kysect.CommonLib.BaseTypes.Extensions;
using System.IO.Abstractions;

namespace Kysect.CommonLib.FileSystem.Extensions;

public static class DirectoryExtensions
{
    public static void EnsureParentDirectoryExists(string filePath)
    {
        filePath.ThrowIfNull();

        var fileInfo = new FileInfo(filePath);
        if (fileInfo.Directory is null)
            throw new ArgumentException($"File {fileInfo.FullName} does not have directory");

        EnsureDirectoryExists(fileInfo.Directory.FullName);
    }

    public static void EnsureDirectoryExists(string path)
    {
        if (path is null)
            throw new ArgumentNullException(nameof(path));

        if (Directory.Exists(path))
            return;

        Directory.CreateDirectory(path);
    }

    public static void EnsureParentDirectoryExists(IFileSystem fileSystem, string path)
    {
        fileSystem.ThrowIfNull();
        path.ThrowIfNull();

        IFileInfo fileInfo = fileSystem.FileInfo.New(path);
        EnsureParentDirectoryExists(fileSystem, fileInfo);
    }

    public static void EnsureParentDirectoryExists(IFileSystem fileSystem, IFileInfo file)
    {
        fileSystem.ThrowIfNull();
        file.ThrowIfNull();

        if (file.Directory is null)
            throw new ArgumentException($"File {file.FullName} does not have directory");

        EnsureDirectoryExists(fileSystem, file.Directory);
    }

    public static void EnsureDirectoryExists(IFileSystem fileSystem, IDirectoryInfo directory)
    {
        fileSystem.ThrowIfNull();
        directory.ThrowIfNull();

        if (fileSystem.Directory.Exists(directory.FullName))
            return;

        fileSystem.Directory.CreateDirectory(directory.FullName);
    }
}