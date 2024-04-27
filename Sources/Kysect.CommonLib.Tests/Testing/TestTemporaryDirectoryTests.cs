using FluentAssertions;
using Kysect.CommonLib.Testing;

namespace Kysect.CommonLib.Tests.Testing;

public class TestTemporaryDirectoryTests
{
    /// <summary>
    /// We use this method during testing of git clone and get VERY LONG path. GitHub may fail with
    /// LibGit2Sharp.LibGit2SharpException : path too long: 'D:/a/GithubUtils/GithubUtils/Sources/artifacts/bin/Kysect.GithubUtils.Tests/release/TempDirectory/0d339f96-3146-42ff-84ee-d2534af2c84b/80ec70c9-6afa-413b-b3af-ac6ecc10aa8f/Sources/Kysect.GithubUtils/Contributions/ActivityProviders/GithubActivityProviderExtensions.cs
    /// </summary>
    [Fact]
    public void GetTemporaryDirectory_ReturnStringDoNotLong()
    {
        var fileSystem = new System.IO.Abstractions.FileSystem();
        string baseDirectoryPath = fileSystem.Path.GetFullPath(".");
        using var temporaryDirectory = new TestTemporaryDirectory(fileSystem);

        string directoryPath = temporaryDirectory.GetTemporaryDirectory();
        int additionalLength = directoryPath.Length - baseDirectoryPath.Length;

        additionalLength.Should().BeLessThan(25);
    }
}