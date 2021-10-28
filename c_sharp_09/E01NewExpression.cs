using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace c_sharp_09
{
    public class E01NewExpression
    {
        private readonly ITestOutputHelper testOutputHelper;

        public E01NewExpression(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void New_can_be_shortened()
        {
            // no need to do new string("jon")
            string name = new("jon");

            Assert.Equal("jon", name);
            Assert.Equal(typeof(string), name.GetType());
        }

        [Fact]
        public void New_can_work_with_collection_and_initialize_it()
        {
            Dictionary<string, int> personWithAge = new()
            {
                ["jon"] = 10,
                ["bob"] = 20,
            };

            Assert.IsType<Dictionary<string, int>>(personWithAge);
            Assert.Equal(2, personWithAge.Count);
        }

        private class SomeObject
        {
            public List<int>? MyListOfNumber;
        }

        [Fact]
        public void Shorter_new_makes_fewer_things_to_type()
        {
            SomeObject someObject = new() { MyListOfNumber = new() };

            Assert.IsType<SomeObject>(someObject);
        }
    }
}
