using Xunit;
using Xunit.Abstractions;

namespace c_sharp_09
{
    public class E07Record_stack_overflow
    {
        private readonly ITestOutputHelper testOutputHelper;

        public E07Record_stack_overflow(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

        private record Twin(string name)
        {
            public Twin? Other { get; set; }
        }

        [Fact]
        public void ToString_causes_stack_overflow()
        {
            var tA = new Twin("A");
            var tB = new Twin("B"); 
            tA.Other = tB;
            tB.Other = tA;

            testOutputHelper.WriteLine($"{tA}");
        }

        [Fact]
        public void Spinning_in_a_circle()
        {
            var tA = new Twin("A");
            var tB = new Twin("B");
            tA.Other = tB;
            tB.Other = tA;

            Assert.Equal(tA, tB.Other);
            // test runs fine - but trying to debug fails
            // set a breakpoint and during debug - try to hover
            // over tA
            // this was an issue with early release but it is fixed now
            Assert.Equal(tA, tA.Other.Other);
        }
    }
}
