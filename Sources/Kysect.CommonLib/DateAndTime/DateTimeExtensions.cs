namespace Kysect.CommonLib.DateAndTime;

public static class DateTimeExtensions
{
    public const string OrderingShortDateFormat = "yyyy-MM-dd";
    public const string OrderingFullDateFormat = "yyyy-MM-dd-HH-mm-ss";

    public static string ToOrderedShortString(this DateTime value)
    {
        return value.ToString(OrderingShortDateFormat);
    }

    public static string ToOrderedFullString(this DateTime value)
    {
        return value.ToString(OrderingFullDateFormat);
    }
}