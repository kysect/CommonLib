namespace Kysect.CommonLib.DateAndTime;

public static class DateFinder
{
    public static DateTimeOffset GetNearestPrevious(DateTimeOffset currentDate, SelectedDayOfWeek mask)
    {
        currentDate = currentDate.Date;
        if (mask == SelectedDayOfWeek.None)
            throw new ArgumentException("Cannot find date by empty mask");

        while (!SelectedDayOfWeekExtensions.Contains(mask, currentDate.DayOfWeek))
        {
            currentDate = currentDate.AddDays(-1);
        }

        return currentDate;
    }

    public static DateTimeOffset GetNearestNext(DateTimeOffset currentDate, SelectedDayOfWeek mask)
    {
        currentDate = currentDate.Date;
        if (mask == SelectedDayOfWeek.None)
            throw new ArgumentException("Cannot find date by empty mask");

        while (!SelectedDayOfWeekExtensions.Contains(mask, currentDate.DayOfWeek))
        {
            currentDate = currentDate.AddDays(1);
        }

        return currentDate;
    }

    public static DateTimeOffset GetPrevious(DateTimeOffset currentDate, SelectedDayOfWeek mask)
    {
        currentDate = currentDate.Date;
        if (mask == SelectedDayOfWeek.None)
            throw new ArgumentException("Cannot find date by empty mask");

        do
        {
            currentDate = currentDate.AddDays(-1);
        } while (!SelectedDayOfWeekExtensions.Contains(mask, currentDate.DayOfWeek));

        return currentDate;
    }
}