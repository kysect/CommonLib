namespace Kysect.CommonLib.DateAndTime;

public class DateTimeIntervalFactory
{
    public IReadOnlyCollection<DateTimeInterval> Create(DateTimeOffset start, DateTimeOffset end, DayOfWeekTime dayOfWeekTime)
    {
        if (start > end)
            throw new ArgumentException("Interval end is lower that start.");

        DateTimeOffset currentStart = dayOfWeekTime.GetNextPointInTime(start);
        var result = new List<DateTimeInterval>();

        if (currentStart > end)
        {
            result.Add(new DateTimeInterval(start, end));
            return result;
        }

        if (start < currentStart)
        {
            result.Add(new DateTimeInterval(start, currentStart));
        }
        else
        {
            currentStart = start;
        }

        while (currentStart.AddDays(7) < end)
        {
            DateTimeOffset intervalEnd = currentStart.AddDays(7);
            result.Add(new DateTimeInterval(currentStart, intervalEnd));

            currentStart = intervalEnd;
        }

        if (currentStart != end)
            result.Add(new DateTimeInterval(currentStart, end));

        return result;
    }
}