using System.Text;

namespace Kysect.CommonLib.FileSystem;

public static class NonLockFileReader
{
    public static IEnumerable<string> ReadAllLines(string path)
    {
        using var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 0x1000, FileOptions.SequentialScan);
        using var sr = new StreamReader(fs, Encoding.UTF8);

        string? line;
        while ((line = sr.ReadLine()) != null)
        {
            yield return line;
        }
    }
}