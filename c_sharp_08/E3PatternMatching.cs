using model.Geo;
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

        private string FormatName(string? firstName, string? middleName, string? lastName) =>
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

        private string RockPaperScissors(string one, string other) =>
            (one, other) switch
            {
                ("paper", "rock") => "one",
                ("rock", "paper") => "other",
                ("scissors", "paper") => "one",
                ("paper", "scissors") => "other",
                ("rock", "scissors") => "one",
                ("scissors", "rock") => "other",
                (_, _) => "tie"
            };

        [Theory]
        [InlineData("paper", "rock", "one")]
        [InlineData("paper", "scissors", "other")]
        public void Rock_paper_scissors(string one, string other, string expected)
        {
            var winnerIs = RockPaperScissors(one, other);

            testOutputHelper.WriteLine($"winner is: {winnerIs}");
            Assert.Equal(expected, winnerIs);
        }

        public enum Quadrant
        {
            Unknown,
            Origin,
            First,
            Second,
            Third,
            Fourth,
            OnAxis
        }

        public Quadrant GetQuadrant(GeoPoint geoPoint) =>
            geoPoint switch
            {
                (0, 0) => Quadrant.Origin,
                var (x, y) when x > 0 && y > 0 => Quadrant.First,
                var (x, y) when x < 0 && y > 0 => Quadrant.Second,
                var (x, y) when x < 0 && y < 0 => Quadrant.Third,
                var (x, y) when x > 0 && y < 0 => Quadrant.Fourth,
                var (_ , _) => Quadrant.OnAxis,
                _ => Quadrant.Unknown
            };

        [Theory]
        [InlineData(0, 0, Quadrant.Origin)]
        [InlineData(1, 1, Quadrant.First)]
        [InlineData(-1, 1, Quadrant.Second)]
        [InlineData(-1, -1, Quadrant.Third)]
        [InlineData(1, -1, Quadrant.Fourth)]
        [InlineData(0, 1, Quadrant.OnAxis)]
        [InlineData(1, 0, Quadrant.OnAxis)]
        [InlineData(0, -1, Quadrant.OnAxis)]
        [InlineData(-1, 0, Quadrant.OnAxis)]
        public void Where_am_i(int x, int y, Quadrant expectedQudrant)
        {
            var geoPoint = new GeoPoint(x, y);

            var whereAmI = GetQuadrant(geoPoint);

            testOutputHelper.WriteLine($"where am i: {whereAmI}");
            Assert.Equal(expectedQudrant, whereAmI);
        }
    }
}
