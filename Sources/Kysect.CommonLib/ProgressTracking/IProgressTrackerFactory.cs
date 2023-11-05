namespace Kysect.CommonLib.ProgressTracking;

public interface IProgressTrackerFactory
{
    IProgressTracker Create(string operationName, int maxValue);
}