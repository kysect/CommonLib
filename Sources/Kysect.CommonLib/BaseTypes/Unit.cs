namespace Kysect.CommonLib.BaseTypes;

#pragma warning disable CA1052 // Static holder types should be Static or NotInheritable
public class Unit
#pragma warning restore CA1052 // Static holder types should be Static or NotInheritable
{
    public static Unit Instance { get; } = new Unit();

    private Unit()
    {
    }
}