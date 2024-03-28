using FluentAssertions;
using Kysect.CommonLib.DateAndTime;

namespace Kysect.CommonLib.Tests.DateAndTime;

public class SelectedDayOfWeekExtensionTests
{
    [Fact]
    public void Contains_ForAllValues_ReturnCorrectMapping()
    {
        foreach (DayOfWeek dayOfWeek in Enum.GetValues<DayOfWeek>())
        {
            SelectedDayOfWeek selectedDayOfWeek = SelectedDayOfWeekExtensions.ConvertToSelectedDayOfWeek(dayOfWeek);

            selectedDayOfWeek.Contains(dayOfWeek).Should().BeTrue();
        }
    }
}