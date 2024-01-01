using Kysect.CommonLib.Internal;
using System.IO.Abstractions;

namespace Kysect.CommonLib.FileSystem;

public record struct RelativePath(string Value)
{
    public static RelativePath CreateFromFull(IFileSystem fileSystem, string directoryPath, string fullPath)
    {
        string relativePath = PathNetCore.GetRelativePath(fileSystem, directoryPath, fullPath);
        return new RelativePath(relativePath);
    }
}