using model.Shapes;
using Xunit;
using Xunit.Abstractions;

namespace c_sharp_07
{
    public class E4PatternMatching2
    {
        private readonly ITestOutputHelper testOutputHelper;

        public E4PatternMatching2(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

        private string WhoAmI_UsingSwitch(Shape shape)
        {
            if (shape == null)
                return "nothing to write about";

            switch (shape)
            {
                case Circle c:
                    return $"circle with name: {c.Name}";

                case Pentagon p:
                    return $"pentagon with name: {p.Name}";

                // note - this has to come before next - else it would never get executed
                case Rectangle r when r.Height == r.Width:
                    return $"square hiding as rectangle with name: {r.Name}";
                // note - this has to come after previous one
                case Rectangle r2:
                    return $"rectangle with name: {r2.Name}";

                case Square s:
                    return $"square with name: {s.Name}";

                case Triangle t:
                    return $"triangle with name: {t.Name}";
            }

            return "have no clue";
        }

        [Fact]
        public void Identify_Circle()
        {
            var circle = new Circle("c1");

            var iAm = WhoAmI_UsingSwitch(circle);

            testOutputHelper.WriteLine($"iAm a: {iAm}");
            Assert.Equal("circle with name: c1", iAm);
        }

        [Fact]
        public void Identify_Pentagon()
        {
            var pentagon = new Pentagon("p1");

            var iAm = WhoAmI_UsingSwitch(pentagon);

            testOutputHelper.WriteLine($"iAm a: {iAm}");
            Assert.Equal("pentagon with name: p1", iAm);
        }

        [Fact]
        public void Identify_Square_hiding_as_Rectangle()
        {
            var fakeRectangle = new Rectangle("r1") { Height = 5, Width = 5 };

            var iAm = WhoAmI_UsingSwitch(fakeRectangle);

            testOutputHelper.WriteLine($"iAm a: {iAm}");
            Assert.Equal("square hiding as rectangle with name: r1", iAm);
        }

        [Fact]
        public void Identify_Rectangle()
        {
            var rectangle = new Rectangle("r2") { Height = 5, Width = 4 };

            var iAm = WhoAmI_UsingSwitch(rectangle);

            testOutputHelper.WriteLine($"iAm a: {iAm}");
            Assert.Equal("rectangle with name: r2", iAm);
        }

        [Fact]
        public void Identify_Square()
        {
            var square = new Square("s1") { Side = 5 };

            var iAm = WhoAmI_UsingSwitch(square);

            testOutputHelper.WriteLine($"iAm a: {iAm}");
            Assert.Equal("square with name: s1", iAm);
        }

        [Fact]
        public void Identify_Triangle()
        {
            var triangle = new Triangle("t1");

            var iAm = WhoAmI_UsingSwitch(triangle);

            testOutputHelper.WriteLine($"iAm a: {iAm}");
            Assert.Equal("triangle with name: t1", iAm);
        }
    }
}
