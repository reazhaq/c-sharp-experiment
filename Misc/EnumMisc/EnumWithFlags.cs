namespace Misc.EnumMisc;

/// <summary>
/// how many enum values are there - not 7 for sure
/// </summary>
[Flags]
public enum Days
{
    Undefined = 0,
    Monday = 1,
    Tuesday = 2,
    Wednesday = 4,
    Thursday = 8,
    Friday = 16,
    Saturday = 32,
    Sunday = 64,
}

/// <summary>
/// when it comes to enum with flags - don't start with zero
/// if you like to start with zero - use "undefined" for zero
/// </summary>
[Flags]
public enum WeekDays
{
    Monday = 0,
    Tuesday = 1,
    Wednesday = 2,
    Thursday = 4,
    Friday = 8,
    Saturday = 16,
    Sunday = 32,
}

public class EnumWithFlags
{
    private readonly ITestOutputHelper testOutputHelper;

    public EnumWithFlags(ITestOutputHelper testOutputHelper)
    {
        this.testOutputHelper = testOutputHelper;
    }

    /// <summary>
    /// how many enum values are there - when it starts with 1
    /// take the last enum number, double it minus 1
    /// </summary>
    [Fact]
    public void Enums_with_Flags_defines_many_more_behind_the_scene()
    {
        for (int i = 0; i < 129; i++)
        {
            testOutputHelper.WriteLine($"enum value for {i:000} => {(Days)i}");
        }
    }

    /// <summary>
    /// same number of enum values between Days and WeekDays
    /// since weekdays starts with zero - it only got half the enums
    /// </summary>
    [Fact]
    public void Enums_with_Flags_defines_many_more_behind_the_scene2()
    {
        for (int i = 0; i < 65; i++)
        {
            testOutputHelper.WriteLine($"enum value for {i:00} => {(WeekDays)i}");
        }
    }

    [Theory]
    [InlineData(Days.Monday)]
    [InlineData(Days.Wednesday)]
    [InlineData(Days.Thursday)]
    [InlineData(Days.Undefined)]
    [InlineData(Days.Monday | Days.Tuesday | Days.Wednesday | Days.Thursday | Days.Friday)]
    [InlineData(Days.Undefined | Days.Friday)]
    public void HowDoesEqualWorks(Days days)
    {
        testOutputHelper.WriteLine($"{Days.Undefined == days}: Days.Undefined == {days}");
        testOutputHelper.WriteLine($"{Days.Monday == days}: Days.Monday == {days}");
        testOutputHelper.WriteLine($"{Days.Tuesday == days}: Days.Tuesday == {days}");
        testOutputHelper.WriteLine($"{Days.Wednesday == days}: Days.Wednesday == {days}");
        testOutputHelper.WriteLine($"{Days.Thursday == days}: Days.Thursday == {days}");
        testOutputHelper.WriteLine($"{Days.Friday == days}: Days.Friday == {days}");
        testOutputHelper.WriteLine($"{Days.Saturday == days}: Days.Saturday == {days}");
        testOutputHelper.WriteLine($"{Days.Sunday == days}: Days.Sunday == {days}");
    }

    [Theory]
    [InlineData(Days.Monday)]
    [InlineData(Days.Wednesday)]
    [InlineData(Days.Thursday)]
    [InlineData(Days.Monday | Days.Tuesday | Days.Wednesday | Days.Thursday | Days.Friday)]
    public void EnumHasFlags(Days days)
    {
        var meetingDays = Days.Tuesday | Days.Thursday;
        testOutputHelper.WriteLine($"{days} has flags: {days.HasFlag(meetingDays)}");
    }

    [Theory]
    [InlineData(Days.Monday)]
    [InlineData(Days.Wednesday)]
    [InlineData(Days.Thursday)]
    [InlineData(Days.Monday | Days.Tuesday | Days.Wednesday | Days.Thursday | Days.Friday)]
    public void EnumHasFlags_doesItHaveUndefined(Days days)
    {
        // here is a problem with enum-with-flags
        // it is recommended to start with Undefined or None with zero value
        // buy trying to use it like is Undefined included always returns zero
        // https://docs.microsoft.com/en-us/dotnet/api/system.enum.hasflag?view=net-6.0#remarks
        // describes to test or Undefined first, then use HasFlags
        // not very clean
        var noClueWhatToDoDays = Days.Undefined;
        testOutputHelper.WriteLine($"is {days} one of 'no clue what to do' day: {days.HasFlag(noClueWhatToDoDays)}");
    }
}
