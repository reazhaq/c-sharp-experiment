using Xunit;
using Xunit.Abstractions;

namespace c_sharp_08
{
    public class E04IndicesAndRanges
    {
        private readonly ITestOutputHelper testOutputHelper;
        private string[] Numbers = new string[] { "zero", "one", "two", "three", "four", "five" };

        public E04IndicesAndRanges(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void LastItem()
        {
            var lastItem = Numbers[^1];

            testOutputHelper.WriteLine($"lastItem: {lastItem}");
            Assert.Equal("five", lastItem);
        }

        [Fact]
        public void SecondThirdAndFourthItems()
        {
            // start from index 1 and go upto index 4
            foreach (var item in Numbers[1..4])
            {
                testOutputHelper.WriteLine(item);
            }
        }
    }
}
