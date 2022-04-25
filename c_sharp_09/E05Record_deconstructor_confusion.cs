using Xunit;
using Xunit.Abstractions;

namespace c_sharp_09
{
    public class E05Record_deconstructor_confusion
    {
        private readonly ITestOutputHelper testOutputHelper;

        public E05Record_deconstructor_confusion(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

        private record Person(string FirstName, string LastName);

        [Fact]
        public void Deconstructor_is_nice()
        {
            var p1 = new Person("ant", "man");

            var (f,l) = p1;

            testOutputHelper.WriteLine($"f: {f}, l: {l}");
            Assert.Equal("ant", f);
            Assert.Equal("man", l);
        }

        private record LoneStar(string Name);

        private class LoneStarClass
        {
            public string Name { get; init; }

            public LoneStarClass(string name)
            {
                Name = name;
            }

            public void Deconstruct(out string name)
            {
                name = Name;
            }
        }

        [Fact]
        public void Deconstructor_is_not_cool()
        {
            var l1 = new LoneStar("hawkeye");

            //// the following doesn't work - 
            //var (n) = l1;
            //testOutputHelper.WriteLine($"n: {n}");
            //Assert.Equal("hawkeye", n);

            //// the following doesn't work for hand rolled class
            //// this is a odd problem with deconstructor and value tuple
            //var l2 = new LoneStarClass("hawkeye");
            //var (n) = l2;

            // this works if you have to use deconstructor
            l1.Deconstruct(out var n);
            testOutputHelper.WriteLine($"n: {n}");
            Assert.Equal("hawkeye", n);
        }
    }
}
