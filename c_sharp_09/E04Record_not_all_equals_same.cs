using System;
using Xunit;

namespace c_sharp_09
{
    public class E04Record_not_all_equals_same
    {
        public record Person(string firstName, string lastName);

        public record Student(string firstName, string lastName, int id) : Person(firstName, lastName);

        [Fact]
        public void EqualsAreConfusing()
        {
            var p1 = new Person("doctor", "strange");
            var s1 = new Student("doctor", "strange", 1);

            var p1EqualsS1 = p1.Equals(s1);
            Assert.False(p1EqualsS1);

            var s1EqualsP1 = s1.Equals(p1);
            Assert.False(s1EqualsP1);

            // this one fails too - that's strange
            // should we not be able to cast to base object
            // and expect them to be equal
            var ps = (Person)s1;
            Assert.False(ps.Equals(p1));
        }

        public class PersonClass : IEquatable<PersonClass>
        {
            public string FirstName { get; init; }
            public string LastName { get; init; }

            public PersonClass(string firstName, string lastName)
            {
                FirstName = firstName;
                LastName = lastName;
            }

            public virtual bool Equals(PersonClass? other)
            {
                if (other == null)
                    return false;

                return FirstName.Equals(other.FirstName) && LastName.Equals(other.LastName);
            }
        }

        public class StudentClass : PersonClass, IEquatable<StudentClass>
        {
            public int Id { get; init; }

            public StudentClass(string firstName, string lastName, int id) : base(firstName, lastName)
            {
                Id = id;
            }

            public bool Equals(StudentClass? other)
            {
                if (other == null)
                    return false;

                return Id == other.Id && base.Equals(other);
            }
        }

        [Fact]
        public void EqualsAre_not_Confusing()
        {
            var p1 = new PersonClass("doctor", "strange");
            var s1 = new StudentClass("doctor", "strange", 1);

            var p1EqualsS1 = p1.Equals(s1);
            Assert.True(p1EqualsS1);

            var s1EqualsP1 = s1.Equals(p1);
            Assert.True(s1EqualsP1);

            var ps = (PersonClass)s1;
            Assert.True(ps.Equals(p1));
        }

    }
}