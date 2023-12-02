namespace Kysect.CommonLib.DateAndTime;

public static class SelectedDayOfWeekExtensions
{
    public static bool Contains(this SelectedDayOfWeek selectedDayOfWeek, DateTime dateTime)
    {
        return Contains(selectedDayOfWeek, dateTime.DayOfWeek);
    }

    public static bool Contains(this SelectedDayOfWeek selectedDayOfWeek, DayOfWeek dayOfWeek)
    {
        return dayOfWeek switch
        {
            DayOfWeek.Monday => selectedDayOfWeek.HasFlag(SelectedDayOfWeek.Monday),
            DayOfWeek.Tuesday => selectedDayOfWeek.HasFlag(SelectedDayOfWeek.Tuesday),
            DayOfWeek.Wednesday => selectedDayOfWeek.HasFlag(SelectedDayOfWeek.Wednesday),
            DayOfWeek.Thursday => selectedDayOfWeek.HasFlag(SelectedDayOfWeek.Thursday),
            DayOfWeek.Friday => selectedDayOfWeek.HasFlag(SelectedDayOfWeek.Friday),
            DayOfWeek.Saturday => selectedDayOfWeek.HasFlag(SelectedDayOfWeek.Saturday),
            DayOfWeek.Sunday => selectedDayOfWeek.HasFlag(SelectedDayOfWeek.Sunday),
            _ => throw new ArgumentOutOfRangeException($"Invalid day of week {dayOfWeek}")
        };
    }

    public static SelectedDayOfWeek ConvertToSelectedDayOfWeek(DayOfWeek dayOfWeek)
    {
        return dayOfWeek switch
        {
            DayOfWeek.Monday => SelectedDayOfWeek.Monday,
            DayOfWeek.Tuesday => SelectedDayOfWeek.Tuesday,
            DayOfWeek.Wednesday => SelectedDayOfWeek.Wednesday,
            DayOfWeek.Thursday => SelectedDayOfWeek.Thursday,
            DayOfWeek.Friday => SelectedDayOfWeek.Friday,
            DayOfWeek.Saturday => SelectedDayOfWeek.Saturday,
            DayOfWeek.Sunday => SelectedDayOfWeek.Sunday,
            _ => throw new ArgumentOutOfRangeException($"Invalid day of week {dayOfWeek}")
        };
    }
}