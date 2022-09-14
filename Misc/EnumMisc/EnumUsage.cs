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
    public Color ColorFromNumber_Incorrect(int number) => (Color)number;

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void Any_Number_Is_a_Color(int number)
    {
        var c = ColorFromNumber_Incorrect(number);
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

    // this is incorrect - cause it magically parses a number-as-text
    // into color and assumes it is valid enum - no runtim error
    public bool TryColorFromString_Incorrect(string colorName, out Color color) =>
        Enum.TryParse(colorName, true, out color);

    [Theory]
    [InlineData("3")]
    [InlineData("7")] // this one is happily converted to color
    [InlineData("red")]
    [InlineData("Red")]
    [InlineData("red,2")]
    [InlineData("red,green")]
    [InlineData("red,white")] // this is interesting - not what you expected
    [InlineData("yellow")]
    [InlineData(null)]
    public void InCorrect_Way_ToConvertTextToColor(string colorName)
    {
        if (TryColorFromString_Incorrect(colorName, out Color color))
            testOutputHelper.WriteLine($"{colorName} converts to color: {color}");
        else
            testOutputHelper.WriteLine($"{colorName} is not a color");
    }

    public bool TryColorFromString(string colorName, out Color color) =>
        Enum.TryParse(colorName, true, out color) && Enum.IsDefined(color);

    [Theory]
    [InlineData("3")]
    [InlineData("7")] // this one is not converted to color
    [InlineData("red")]
    [InlineData("Red")]
    [InlineData("red,2")]
    [InlineData("red,green")]
    [InlineData("red,white")] // this is interersting - not what you expected
    [InlineData("yellow")]
    [InlineData(null)]
    public void Correct_Way_ToConvertTextToColor(string colorName)
    {
        if (TryColorFromString(colorName, out Color color))
            testOutputHelper.WriteLine($"{colorName} converts to color: {color}");
        else
            testOutputHelper.WriteLine($"{colorName} is not a color");
    }

    // two problems with this method:
    // 1st: it assumes only 4 enum values and if first 3 if fails - it must be "blue".
    //  this intimate knowledge/tight-coupling forces updates in multiple similar places
    //  if a color gets added to enum
    // 2nd problem - caller can send anything, like a large or negative number and it shall assume
    //  this to be "blue"
    // btw - this sort of multiple if statements is a code smell
    public string CommonMistakeUsingEnum(Color color)
    {
        if (color == Color.Undefined)
            return "undefined";
        if (color == Color.Red)
            return "red";
        if (color == Color.White)
            return "white";

        return "blue";
    }

    [Theory]
    [InlineData(Color.Red)]
    [InlineData(Color.White)]
    [InlineData((Color)(-7))]
    [InlineData((Color)9)]
    public void UsingCommonMistake(Color color)
    {
        testOutputHelper.WriteLine($"{color} => {CommonMistakeUsingEnum(color)}");
    }
}
