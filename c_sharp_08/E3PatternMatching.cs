using Xunit;
using Xunit.Abstractions;

namespace c_sharp_08
{
    public class E3PatternMatching
    {
        private readonly ITestOutputHelper testOutputHelper;

        public E3PatternMatching(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

        public enum Colors
        {
            Red,
            Green,
            Blue,            
        }

        public string ColorText(Colors colors) =>
            colors switch
            {
                Colors.Red => "red",
                Colors.Green => "green",
                Colors.Blue => "blue",
                _ => "have no clue"
            };

        [Theory]
        [InlineData(Colors.Red, "red")]
        public void Color_text_using_pattern_matching_switch(Colors colors, string expected)
        {
            var colorText = ColorText(colors);

            testOutputHelper.WriteLine(colorText);
            Assert.Equal(expected, colorText);
        }

        public string FormatName(string? firstName, string? middleName, string? lastName) =>
            (firstName, middleName, lastName) switch
            {
                (null,      null,       null)       => "get yourself a name",
                (string f,  string m,   null)       => $"_, {f} {m}",
                (string f,  null,       string l)   => $"{l}, {f} _",
                (null,      string m,   string l)   => $"{l}, _ {m}",
                (string f,  null,       null)       => $"_, {f} _",
                (null,      null,       string l)   => $"{l}, _ _",
                (null,      string m,   null)       => $"_, _ {m}",
                (string f,  string m,   string l)   => $"{l}, {f} {m}"
            };

        [Theory]
        [InlineData(null, null, null, "get yourself a name")]
        [InlineData("first", "middle", null, "_, first middle")]
        [InlineData("first", null, "last", "last, first _")]
        [InlineData(null, "middle", "last", "last, _ middle")]
        [InlineData("first", null, null, "_, first _")]
        [InlineData(null, null, "last", "last, _ _")]
        [InlineData(null, "middle", null, "_, _ middle")]
        [InlineData("first", "middle", "last", "last, first middle")]
        public void Format_name_using_pattern_matching_switch(string f, string m, string l, string expected)
        {
            var result = FormatName(f, m, l);

            testOutputHelper.WriteLine($"for {f ?? "null"} {m ?? "null"} {l ?? "null"} => {result}");
            Assert.Equal(expected, result);
        }
    }
}
