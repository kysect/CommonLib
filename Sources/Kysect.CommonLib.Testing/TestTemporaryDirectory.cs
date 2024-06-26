﻿using Kysect.CommonLib.BaseTypes.Extensions;
using Kysect.CommonLib.FileSystem;
using System.IO.Abstractions;

namespace Kysect.CommonLib.Testing;

public class TestTemporaryDirectory : IDisposable
{
    private readonly System.IO.Abstractions.FileSystem _fileSystem;
    private readonly IDirectoryInfo _directoryInfo;

    public TestTemporaryDirectory(System.IO.Abstractions.FileSystem fileSystem, string rootPath = ".")
    {
        _fileSystem = fileSystem.ThrowIfNull();
        rootPath.ThrowIfNull();

        string path = _fileSystem.Path.Combine(rootPath, "Temp", GetRandomDirectoryName());

        if (_fileSystem.Directory.Exists(path))
        {
            IDirectoryInfo directoryInfo = _fileSystem.DirectoryInfo.New(path);
            DeleteRecursive(directoryInfo);
        }

        _directoryInfo = _fileSystem.Directory.CreateDirectory(path);
        fileSystem.EnsureDirectoryExists(path);
    }

    public string GetTemporaryDirectory()
    {
        return _fileSystem.Path.Combine(_directoryInfo.FullName, GetRandomDirectoryName());
    }

    public void Dispose()
    {
        DeleteRecursive(_directoryInfo);
    }

    // KB: https://github.com/libgit2/libgit2sharp/issues/1354
    public static void DeleteRecursive(IDirectoryInfo target)
    {
        target.ThrowIfNull();

        if (!target.Exists)
        {
            return;
        }

        foreach (var file in target.EnumerateFiles())
        {
            if (file.IsReadOnly)
            {
                file.IsReadOnly = false;
            }

            file.Delete();
        }

        foreach (IDirectoryInfo dir in target.EnumerateDirectories())
        {
            DeleteRecursive(dir);
        }


        target.Delete();
    }

    private string GetRandomDirectoryName()
    {
        return Guid.NewGuid().ToString().Substring(0, 8);
    }
}