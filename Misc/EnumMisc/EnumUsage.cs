namespace Misc.EnumMisc;

/// <summary>
/// starting with undefined is a good idea; specially when working with flags
/// also enum is just a number behind the scene and default for int is zero
/// </summary>
public enum Colors
{
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

    /// <summary>
    /// since casting a number to enum shows no-error during compile time or run time
    /// using just casting to convert a number to color is not a good idea
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    public Colors ColorFromNumber_Incorrect(int number) => (Colors)number;

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void InCorrect_Way_ToConvert_NumberToEnum(int number)
    {
        var c = ColorFromNumber_Incorrect(number);
        testOutputHelper.WriteLine($"{number} is color: {c}");
    }

    /// <summary>
    /// correct way to check
    /// </summary>
    /// <param name="number"></param>
    /// <param name="color"></param>
    /// <returns></returns>
    public bool TryColorFromNumber_Correct(int number, out Colors color)
    {
        color = (Colors)number;
        return Enum.IsDefined(color);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void Correct_Way_ToConvert_NumberToEnum(int number)
    {
        if (TryColorFromNumber_Correct(number, out Colors color))
            testOutputHelper.WriteLine($"{number} is color: {color}");
        else
            testOutputHelper.WriteLine($"{number} is not a color");
    }

    /// <summary>
    /// this is incorrect - cause it magically parses a number-as-text
    /// into color and assumes it is valid enum - no runtim error
    /// </summary>
    /// <param name="colorName"></param>
    /// <param name="color"></param>
    /// <returns></returns>
    public bool TryColorFromString_Incorrect(string colorName, out Colors color) =>
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
    public void InCorrect_Way_ToConvert_TextToColor(string colorName)
    {
        if (TryColorFromString_Incorrect(colorName, out Colors color))
            testOutputHelper.WriteLine($"{colorName} converts to color: {color}");
        else
            testOutputHelper.WriteLine($"{colorName} is not a color");
    }

    /// <summary>
    /// this is the correct way
    /// </summary>
    /// <param name="colorName"></param>
    /// <param name="color"></param>
    /// <returns></returns>
    public bool TryColorFromString(string colorName, out Colors color) =>
        Enum.TryParse(colorName, true, out color) && Enum.IsDefined(color);

    [Theory]
    [InlineData("3")]
    [InlineData("7")] // this one is not converted to color
    [InlineData("red")]
    [InlineData("Red")]
    [InlineData("red,2")]
    [InlineData("red,green")]
    [InlineData("red,white")] // still not what you expected
    [InlineData("yellow")]
    [InlineData(null)]
    public void Correct_Way_ToConvert_TextToColor(string colorName)
    {
        if (TryColorFromString(colorName, out Colors color))
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
    public string CommonMistakeUsingEnum(Colors color)
    {
        if (color == Colors.Undefined)
            return "undefined";
        if (color == Colors.Red)
            return "red";
        if (color == Colors.White)
            return "white";

        return "blue";
    }

    [Theory]
    [InlineData(Colors.Red)]
    [InlineData(Colors.White)]
    [InlineData((Colors)(-7))]
    [InlineData((Colors)9)]
    public void UsingCommonMistake(Colors color)
    {
        testOutputHelper.WriteLine($"{color} => {CommonMistakeUsingEnum(color)}");
    }
}
