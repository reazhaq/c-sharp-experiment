using System;
using Xunit;
using Xunit.Abstractions;

namespace c_sharp_7
{
    /// <summary>
    /// old style tuple was ref type - new one is value type
    /// old one had to be used as item1, item2, etc.
    /// new one - items can have names
    /// </summary>
    public class E1TuplesAndDiscards
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public E1TuplesAndDiscards(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Old_styles_tuples_are_clunky()
        {
            var person = new Tuple<string, int>("john doe", 37);

            // but they are accessed using Item1, Item2, ... not so user friendly
            Assert.Equal("john doe", person.Item1);
            Assert.Equal(37, person.Item2);
            _testOutputHelper.WriteLine($"{person.GetType()}");
            Assert.Equal(typeof(Tuple<string, int>), person.GetType());
        }

        [Fact]
        public void ValueTuple_with_named_properties()
        {
            // tuple can be declared like this
            (string Name, int Age) person = ("john doe", 37);

            Assert.Equal("john doe", person.Name);
            Assert.Equal(37, person.Age);
            _testOutputHelper.WriteLine($"{person.GetType()}");
            Assert.Equal(typeof(ValueTuple<string, int>), person.GetType());
        }

        [Fact]
        public void ValueTuple_with_named_properties2()
        {
            // tuple can be declared like this
            var person = (Name: "john doe", Age: 37);

            Assert.Equal("john doe", person.Name);
            Assert.Equal(37, person.Age);
            _testOutputHelper.WriteLine($"{person.GetType()}");
            Assert.Equal(typeof(ValueTuple<string, int>), person.GetType());
        }

        [Fact]
        public void ValueTuple_with_named_properties3()
        {
            // tuple can be declared like this - old style
            var person = ("john doe", 37);

            // but they are accessed using Item1, Item2, ... not so user friendly
            Assert.Equal("john doe", person.Item1);
            Assert.Equal(37, person.Item2);
            _testOutputHelper.WriteLine($"{person.GetType()}");
            Assert.Equal(typeof(ValueTuple<string, int>), person.GetType());
        }

        [Fact]
        public void Function_return_ValueTuple_old_style()
        {
            var person = GetPerson();

            // but they are accessed using Item1, Item2, ... not so user friendly
            Assert.Equal("john doe", person.Item1);
            Assert.Equal(37, person.Item2);
            _testOutputHelper.WriteLine($"{person.GetType()}");
            Assert.Equal(typeof(ValueTuple<string, int>), person.GetType());
        }

        // returns old style tuple but value type
        (string, int) GetPerson()
        {
            return ("john doe", 37);
        }

        [Fact]
        public void Function_return_ValueTuple_with_names()
        {
            var person = GetPerson2();

            Assert.Equal("john doe", person.Name);
            Assert.Equal(37, person.Age);
            _testOutputHelper.WriteLine($"{person.GetType()}");
            Assert.Equal(typeof(ValueTuple<string, int>), person.GetType());
        }

        (string Name, int Age) GetPerson2()
        {
            return ("john doe", 37);
        }

        [Fact]
        public void Nice_way_to_return_anonymous_type_looking_object_but_value_type()
        {
            var anObject = GetCustomerRecord();

            Assert.Equal("first", anObject.firstName);
            Assert.Equal("last", anObject.lastName);
            Assert.Equal(24, anObject.age);
            _testOutputHelper.WriteLine($"{anObject.GetType()}{Environment.NewLine}");
            Assert.Equal(typeof(ValueTuple<string, string, int>), anObject.GetType());

            var aObject2 = GetCustomerRecord2();
            _testOutputHelper.WriteLine($"aObject2.GetType(): {aObject2.GetType()}{Environment.NewLine}");
            // aObject2.  <- this doesn't work
        }

        [Fact]
        public void Discard_allows_us_to_ignore_return_value()
        {
            // use underscore as place holder variable that you can't use later
            var (fn, _, _) = GetCustomerRecord();

            Assert.NotNull(fn);
        }

        ///////////////////////////////////////////////////////////////////////
        /// additional side notes

        // nice way to return anonymous type looking object; but a value type
        (string firstName, string lastName, int age) GetCustomerRecord()
        {
            // this anonObject can't be returned
            var anonObject = new {fn = "first", ln = "last", a = 24};
            _testOutputHelper.WriteLine($"anonObject.GetType(): {anonObject.GetType()}{Environment.NewLine}");

            // but data can be returned as value tuple
            return (anonObject.fn, anonObject.ln, anonObject.a);
        }

        // side note - you can return an object but past that you are stuck with reflection
        object GetCustomerRecord2()
        {
            return new { fn = "first", ln = "last", a = 24 };
        }
    }
}