namespace Kysect.CommonLib.BaseTypes.Extensions;

public static class EnumExtensions
{
    public static int AsInt<TEnum>(this TEnum value) where TEnum : Enum
    {
        return (int) (object) value;
    }

    public static TEnum AsEnum<TEnum>(this int value) where TEnum : Enum
    {
        return (TEnum) (object) value;
    }
}