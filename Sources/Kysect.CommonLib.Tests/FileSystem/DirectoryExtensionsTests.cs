using FluentAssertions;
using Kysect.CommonLib.FileSystem;
using System.IO.Abstractions.TestingHelpers;

namespace Kysect.CommonLib.Tests.FileSystem;

public class DirectoryExtensionsTests
{
    private readonly MockFileSystem _fileSystem;

    public DirectoryExtensionsTests()
    {
        _fileSystem = new MockFileSystem();
    }

    [Fact]
    public void EnsureParentDirectoryExists_ForNonExistingDirectory_CreateDirectory()
    {
        const string directoryPath = "Dir";
        string filePath = _fileSystem.Path.Combine(directoryPath, "File.txt");

        _fileSystem.Directory.Exists(directoryPath).Should().BeFalse();
        DirectoryExtensions.EnsureParentDirectoryExists(_fileSystem, filePath);
        _fileSystem.Directory.Exists(directoryPath).Should().BeTrue();
    }
}