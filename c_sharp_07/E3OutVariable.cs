using Xunit.Abstractions;
using Xunit;

namespace c_sharp_07
{
    public class E3OutVariable
    {
        private readonly ITestOutputHelper testOutputHelper;

        public E3OutVariable(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Use_of_Old_style_out_variable()
        {
            var string99 = "99";
            int int99;

            var parseResult = int.TryParse(string99, out int99);

            Assert.True(parseResult);
            Assert.Equal(99, int99);
        }

        [Fact]
        public void Use_of_New_style_out_variable()
        {
            var string99 = "99";

            var parseResult = int.TryParse(string99, out var int99);

            Assert.True(parseResult);
            Assert.Equal(99, int99); // note: inline variable is still in scope
        }
    }
}
