using FluentAssertions;
using Kysect.CommonLib.FileSystem.Extensions;
using System.IO.Abstractions.TestingHelpers;

namespace Kysect.CommonLib.Tests.FileSystem;

public class DirectoryExtensionsTests
{
    private MockFileSystem _fileSystem;

    [SetUp]
    public void Setup()
    {
        _fileSystem = new MockFileSystem();
    }

    [Test]
    public void EnsureParentDirectoryExists_ForNonExistingDirectory_CreateDirectory()
    {
        const string directoryPath = "Dir";
        string filePath = _fileSystem.Path.Combine(directoryPath, "File.txt");

        _fileSystem.Directory.Exists(directoryPath).Should().BeFalse();
        DirectoryExtensions.EnsureParentDirectoryExists(_fileSystem, filePath);
        _fileSystem.Directory.Exists(directoryPath).Should().BeTrue();
    }
}