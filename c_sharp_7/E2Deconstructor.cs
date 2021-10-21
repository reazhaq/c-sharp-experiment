using Xunit.Abstractions;
using Xunit;
using System;

namespace c_sharp_7
{
    /// <summary>
    /// deconstructor is not destructor/finalizer
    /// idea is one quick way to deconstruct important inner values of an object.
    /// </summary>
    public class E2Deconstructor
    {
        private readonly ITestOutputHelper testOutputHelper;

        public E2Deconstructor(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

        private class Person
        {
            public string FirstName { get; private set; }
            public string LastName { get; private set; }
            public DateTime DateOfBirth { get; private set; }

            public Person(string FirstName, string LastName, DateTime dob)
            {
                this.FirstName = FirstName;
                this.LastName = LastName;
                this.DateOfBirth = dob;
            }

            public void Deconstruct(out string FirstName, out string LastName, out int Age)
            {
                FirstName = this.FirstName;
                LastName = this.LastName;
                Age = DateTime.Now.Year - DateOfBirth.Year;
            }
        }

        [Fact]
        public void Deconstructor_returns_values_in_one_call()
        {
            var person = new Person("john", "doe", new DateTime(1990, 1, 1));

            person.Deconstruct(out var fn, out var ln, out var age);

            testOutputHelper.WriteLine($"fn: {fn}, ln: {ln}, age: {age}");
            Assert.Equal("john", fn);
            Assert.Equal("doe", ln);
            Assert.Equal((DateTime.Now.Year - 1990), age);
        }

        [Fact]
        public void Deconstructor_plays_nicely_with_ValueTuple()
        {
            var person = new Person("john", "doe", new DateTime(1990, 1, 1));

            var (fn, ln, age) = person;

            testOutputHelper.WriteLine($"(fn, ln, age).GetType(): {(fn, ln, age).GetType()}");
            testOutputHelper.WriteLine($"fn: {fn}, ln: {ln}, age: {age}");
            Assert.Equal("john", fn);
            Assert.Equal("doe", ln);
            Assert.Equal((DateTime.Now.Year - 1990), age);
        }
    }
}
