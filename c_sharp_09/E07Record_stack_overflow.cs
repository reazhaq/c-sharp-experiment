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

        // this is stack overflow on a to-string is by design
        // https://github.com/dotnet/roslyn/issues/49396
        [Fact(Skip = "stack-overflow")]
        public void ToString_causes_stack_overflow()
        {
            var wanda = new Twin("Wanda");
            var pietro = new Twin("Pietro"); 
            wanda.Other = pietro;
            pietro.Other = wanda;

            testOutputHelper.WriteLine($"{wanda}");
        }

        [Fact]
        public void Spinning_in_a_circle()
        {
            var wanda = new Twin("Wanda");
            var pietro = new Twin("Pietro");
            wanda.Other = pietro;
            pietro.Other = wanda;

            Assert.Equal(wanda, pietro.Other);
            // this was an issue with early release but it is fixed now
            // test runs fine - but trying to debug fails
            // set a breakpoint and during debug - try to hover
            // over wanda
            Assert.Equal(wanda, wanda.Other.Other);
        }

        [Fact]
        public void Twin_makes_new_records()
        {
            var wanda = new Twin("Wanda");
            var pietro = new Twin("Pietro");

            // what's going on here...
            // does these assignments create new objects?
            wanda.Other = pietro;
            pietro.Other = wanda;
        }

        /// <summary>
        /// sharplab generated code - this causes stack overflow
        /// go to https://sharplab.io/
        /// enter the following code
        /// var wanda = new Twin("Wanda");
        /// var pietro = new Twin(name: "Pietro");
        /// 
        /// wanda.Other = pietro;
        /// pietro.Other = pietro;
        /// 
        /// System.Console.Write($"{wanda == wanda.Other.Other}");
        /// 
        /// public record Twin(string name)
        /// {
        ///     public Twin? Other { get; set; }
        /// }
        /// 
        /// now look at the generated main function
        /// try to run it - it fails
        /// </summary>
        [Fact(Skip = "sharplab generated code - stack overflow")]
        public void Twin_makes_new_records2()
        {
            Twin twin = new Twin("Wanda");
            Twin twin3 = (twin.Other = new Twin("Pietro"));
            twin3.Other = twin3;
        }
    }
}
