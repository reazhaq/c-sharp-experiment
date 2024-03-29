﻿using model.Shapes;
using Xunit;
using Xunit.Abstractions;

namespace c_sharp_07
{
    /// <summary>
    /// pattern matching using if-then-else
    /// don't have to check the type explicitly
    /// </summary>
    public class E04PatternMatching1
    {
        private readonly ITestOutputHelper testOutputHelper;

        public E04PatternMatching1(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

        private string WhoAmI_UsingIfStatements(Shape shape)
        {
            if (shape is null)
                return "nothing to write about";

            if (shape is Circle c)
                return $"circle with name: {c.Name}";

            if (shape is Pentagon p)
                return $"pentagon with name: {p.Name}";

            // note - this has to come before next - else it would never get executed
            if (shape is Rectangle r && r.Height == r.Width)
                return $"square hiding as rectangle with name: {r.Name}";
            // note - this has to come after previous one
            if (shape is Rectangle r2)
                return $"rectangle with name: {r2.Name}";

            if (shape is Square s)
                return $"square with name: {s.Name}";

            if (shape is Triangle t)
                return $"triangle with name: {t.Name}";

            return "have no clue";
        }

        [Fact]
        public void Identify_Circle()
        {
            var circle = new Circle("c1");

            var iAm = WhoAmI_UsingIfStatements(circle);

            testOutputHelper.WriteLine($"iAm a: {iAm}");
            Assert.Equal("circle with name: c1", iAm);
        }

        [Fact]
        public void Identify_Pentagon()
        {
            var pentagon = new Pentagon("p1");

            var iAm = WhoAmI_UsingIfStatements(pentagon);

            testOutputHelper.WriteLine($"iAm a: {iAm}");
            Assert.Equal("pentagon with name: p1", iAm);
        }

        [Fact]
        public void Identify_Square_hiding_as_Rectangle()
        {
            var fakeRectangle = new Rectangle("r1") { Height = 5, Width = 5 };

            var iAm = WhoAmI_UsingIfStatements(fakeRectangle);

            testOutputHelper.WriteLine($"iAm a: {iAm}");
            Assert.Equal("square hiding as rectangle with name: r1", iAm);
        }

        [Fact]
        public void Identify_Rectangle()
        {
            var rectangle = new Rectangle("r2") { Height = 5, Width = 4 };

            var iAm = WhoAmI_UsingIfStatements(rectangle);

            testOutputHelper.WriteLine($"iAm a: {iAm}");
            Assert.Equal("rectangle with name: r2", iAm);
        }

        [Fact]
        public void Identify_Square()
        {
            var square = new Square("s1") { Side = 5 };

            var iAm = WhoAmI_UsingIfStatements(square);

            testOutputHelper.WriteLine($"iAm a: {iAm}");
            Assert.Equal("square with name: s1", iAm);
        }

        [Fact]
        public void Identify_Triangle()
        {
            var triangle = new Triangle("t1");

            var iAm = WhoAmI_UsingIfStatements(triangle);

            testOutputHelper.WriteLine($"iAm a: {iAm}");
            Assert.Equal("triangle with name: t1", iAm);
        }
    }
}
