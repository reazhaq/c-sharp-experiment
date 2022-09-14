namespace Misc.EnumMisc;

public enum Color
{
    // starting with undefined is a good idea; specially when working with legacy code
    Undefined = 0,
    Red,
    White,
    Blue
}

public class EnumUsage
{
    private readonly ITestOutputHelper testOutputHelper;

    public EnumUsage(ITestOutputHelper testOutputHelper)
    {
        this.testOutputHelper = testOutputHelper;
    }

    // since casting a number to enum shows no-error during compile time or run time
    // using just casting to convert a number to color is not a good idea
    public Color ColorFromNumberIncorrect(int number) => (Color)number;

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void Any_Number_Is_a_Color(int number)
    {
        var c = ColorFromNumberIncorrect(number);
        testOutputHelper.WriteLine($"{number} is color: {c}");
    }

    // correct way to check
    public bool TryColorFromNumber(int number, out Color color)
    {
        color = (Color)number;
        return Enum.IsDefined(color);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void CorrectWayToConvertNumberToEnum(int number)
    {
        if (TryColorFromNumber(number, out Color color))
            testOutputHelper.WriteLine($"{number} is color: {color}");
        else
            testOutputHelper.WriteLine($"{number} is not a color");
    }
}
