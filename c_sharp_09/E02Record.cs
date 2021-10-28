using Xunit.Abstractions;
using Xunit;

namespace c_sharp_09
{
    public class E02Record
    {
        private readonly ITestOutputHelper testOutputHelper;

        public E02Record(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

        private record Person(string FirstName, string LastName);
        //////// this is same as - but much more
        //////private class Person
        //////{
        //////    public string FirstName {  get; set; }
        //////    public string LastName {  get; set; }
        //////}

        [Fact]
        public void Record_is_a_ref_type()
        {
            var p = new Person("jon", "doe");

            Assert.False(p.GetType().IsValueType);
            Assert.True(p.GetType().IsClass);
        }

        [Fact]
        public void Record_got_auto_property()
        {
            var p = new Person("jon", "doe");

            Assert.Equal("jon", p.FirstName);
            Assert.Equal("doe", p.LastName);
        }
    }
}
