using Kysect.CommonLib.BaseTypes.Extensions;
using System.IO.Abstractions;
using System.Text;

namespace Kysect.CommonLib.FileSystem;

public static class NonLockFileReader
{
    public static IEnumerable<string> ReadAllLines(IFileSystem fileSystem, string path)
    {
        fileSystem.ThrowIfNull();
        path.ThrowIfNull();

        using var fs = fileSystem.FileStream.New(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 0x1000, FileOptions.SequentialScan);
        using var sr = new StreamReader(fs, Encoding.UTF8);

        string? line;
        while ((line = sr.ReadLine()) != null)
        {
            yield return line;
        }
    }
}