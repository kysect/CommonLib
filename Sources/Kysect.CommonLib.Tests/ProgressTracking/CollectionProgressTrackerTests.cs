using FluentAssertions;
using Kysect.CommonLib.Logging;
using Kysect.CommonLib.ProgressTracking;
using Microsoft.Extensions.Logging;

namespace Kysect.CommonLib.Tests.ProgressTracking;

public class CollectionProgressTrackerTests
{
    private LoggerProgressTrackerFactory _loggerProgressTrackerFactory;
    private StringBuilderLogger<CollectionProgressTrackerTests> _stringBuilderLogger;

    [SetUp]
    public void Setup()
    {
        var logLevel = LogLevel.Trace;
        _stringBuilderLogger = new StringBuilderLogger<CollectionProgressTrackerTests>(logLevel);
        _loggerProgressTrackerFactory = new LoggerProgressTrackerFactory(_stringBuilderLogger, logLevel);
    }

    [Test]
    public void Select_ForThreeElement_GenerateThreeRow()
    {
        var collectionProgressTracker = new CollectionProgressTracker<int>(_loggerProgressTrackerFactory, [1, 2, 3]);

        collectionProgressTracker = collectionProgressTracker.Select("Select", i => i + 1);

        collectionProgressTracker.Values.Should().BeEquivalentTo([2, 3, 4]);
        _stringBuilderLogger.Build().Should().BeEquivalentTo(["[Select] Processed 1/3", "[Select] Processed 2/3", "[Select] Processed 3/3"]);
    }

    [Test]
    public void SelectParallel_ForThreeElement_GenerateThreeRow()
    {
        var collectionProgressTracker = new CollectionProgressTracker<int>(_loggerProgressTrackerFactory, [1, 2, 3]);

        collectionProgressTracker = collectionProgressTracker.SelectParallel("Select", i => i + 1);

        collectionProgressTracker.Values.Should().BeEquivalentTo([2, 3, 4]);
        _stringBuilderLogger.Build().Should().BeEquivalentTo(["[Select] Processed 1/3", "[Select] Processed 2/3", "[Select] Processed 3/3"]);
    }

    [Test]
    public void SelectMany_ForThreeElement_GenerateThreeRow()
    {
        var collectionProgressTracker = new CollectionProgressTracker<int>(_loggerProgressTrackerFactory, [1, 2, 3]);

        collectionProgressTracker = collectionProgressTracker.SelectMany("Select", i => new[] { i });

        collectionProgressTracker.Values.Should().BeEquivalentTo([1, 2, 3]);
        _stringBuilderLogger.Build().Should().BeEquivalentTo(["[Select] Processed 1/3", "[Select] Processed 2/3", "[Select] Processed 3/3"]);
    }

    [Test]
    public void SelectManyParallel_ForThreeElement_GenerateThreeRow()
    {
        var collectionProgressTracker = new CollectionProgressTracker<int>(_loggerProgressTrackerFactory, [1, 2, 3]);

        collectionProgressTracker = collectionProgressTracker.SelectManyParallel("Select", i => new[] { i });

        collectionProgressTracker.Values.Should().BeEquivalentTo([1, 2, 3]);
        _stringBuilderLogger.Build().Should().BeEquivalentTo(["[Select] Processed 1/3", "[Select] Processed 2/3", "[Select] Processed 3/3"]);
    }

    [Test]
    public void Where_ForThreeElement_GenerateThreeRow()
    {
        var collectionProgressTracker = new CollectionProgressTracker<int>(_loggerProgressTrackerFactory, [1, 2, 3]);

        collectionProgressTracker = collectionProgressTracker.Where("Select", i => i != 3);

        collectionProgressTracker.Values.Should().BeEquivalentTo([1, 2]);
        _stringBuilderLogger.Build().Should().BeEquivalentTo(["[Select] Processed 1/3", "[Select] Processed 2/3", "[Select] Processed 3/3"]);
    }

    [Test]
    public void ApplyParallel_ForThreeElement_GenerateThreeRow()
    {
        var collectionProgressTracker = new CollectionProgressTracker<int>(_loggerProgressTrackerFactory, [1, 2, 3]);

        collectionProgressTracker = collectionProgressTracker.ApplyParallel("Select", i => i++);

        collectionProgressTracker.Values.Should().BeEquivalentTo([1, 2, 3]);
        _stringBuilderLogger.Build().Should().BeEquivalentTo(["[Select] Processed 1/3", "[Select] Processed 2/3", "[Select] Processed 3/3"]);
    }
}