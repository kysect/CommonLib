namespace Kysect.CommonLib.DateAndTime;

public record struct DayOfWeekTime(DayOfWeek DayOfWeek, int Hours, int Minutes)
{
    public readonly DateTimeOffset GetNextPointInTime(DateTimeOffset from)
    {
        if (from.DayOfWeek == DayOfWeek)
        {
            if (from.Hour < Hours || (from.Hour == Hours && from.Minute <= Minutes))
                return from.Date.AddHours(Hours).AddMinutes(Minutes);
        }

        SelectedDayOfWeek selectedDayOfWeek = SelectedDayOfWeekExtensions.ConvertToSelectedDayOfWeek(DayOfWeek);
        DateTimeOffset dateTimeLocal = DateFinder.GetNearestNext(from, selectedDayOfWeek);
        return dateTimeLocal.Date.AddHours(Hours).AddMinutes(Minutes);
    }
}