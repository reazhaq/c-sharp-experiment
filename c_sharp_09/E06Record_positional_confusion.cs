using Xunit;
using Xunit.Abstractions;

namespace c_sharp_09
{
    public class E06Record_positional_confusion
    {
        private readonly ITestOutputHelper testOutputHelper;

        public E06Record_positional_confusion(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

        private record Person(string FirstName, string LastName);

        private record OtherPerson(string LastName, string FirstName) : Person(FirstName, LastName);

        [Fact]
        public void What_does_our_deconstructors_say()
        {
            var o1 = new OtherPerson("man", "super");

            // dev may expect polymorphism here but it returns positional
            var (f1, l1) = (Person)o1;
            testOutputHelper.WriteLine($"(Person)o1 => f1: {f1}, l1: {l1}");

            // this also return positional
            var (f2, l2) = o1;
            testOutputHelper.WriteLine($"f2: {f2}, l2: {l2}");
        }
    }
}
