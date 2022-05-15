namespace c_sharp_11;

public class E01StringInterpolation
{
    private readonly ITestOutputHelper testOutputHelper;

    public E01StringInterpolation(ITestOutputHelper testOutputHelper)
    {
        this.testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void LineBreak()
    {
        // previously line break in the middle of interpolated string-filler was not allowed
        var nowIs = DateTime.Now;

        // now you can - and makes it easier to read/write/manage and not deal with big long string
        testOutputHelper.WriteLine($"short format now is: {nowIs
            .ToShortDateString()} and en-GB format is {nowIs
            .ToString("d", new CultureInfo("en-GB"))}");

        // if you want to line break in the middle of the string - it has to be multiple string
        // as before or start with @ sign
        testOutputHelper.WriteLine($"short format now is: {nowIs.ToShortDateString()} " +
            $"and en-GB format is {nowIs
            .ToString("d", new CultureInfo("en-GB"))}");
    }

    /// <summary>
    /// here is a very common looking json - and let's say we would like use this in code (including line break and indentation)
    /// </summary>
    //{
    //    "fn":"iron",
    //    "ln":"man"
    //}

    [Fact]
    public void ComplexJsonStringWithFormat()
    {
        var p = $"{{{Environment.NewLine}\t\"fn\":\"iron\",{Environment.NewLine}\t\"ln\":\"man\"{Environment.NewLine}}}";
        testOutputHelper.WriteLine(p);
    }

    [Fact]
    public void SimpleJsonStringWithFormat()
    {
        var p =
            """
            {
                "fn":"iron",
                "ln":"man"
            }
            """;
        testOutputHelper.WriteLine(p);
    }

    [Fact]
    public void SimpleJsonStringWithInterpolatedString()
    {
        var fn = "wonder";
        var p =
            $$"""
            {
                "fn":{{fn}}",
                "ln":"woman"
            }
            """;
        testOutputHelper.WriteLine(p);
    }

    [Fact]
    public void WhatIfIWantMultipleQuotesInsideJson()
    {
        var p =
            """"""
            {
                "fn":"captain",
                "ln":"marvel",
                "repeat":"""""
            }
            """""";
        testOutputHelper.WriteLine(p);
    }
}
