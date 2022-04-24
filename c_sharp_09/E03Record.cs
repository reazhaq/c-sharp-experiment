using System;
using System.Collections.Generic;
using Xunit;

namespace c_sharp_09
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/proposals/csharp-9.0/records
    /// </summary>

    public class E03Record
    {
        public sealed record PersonWithHobbies
        {
            public string FirstName { get; init; }
            public string LastName { get; init; }
            public IReadOnlyCollection<string> Hobbies { get; init; }

            public PersonWithHobbies(string firstName, string lastName, List<string> hobbies)
            {
                FirstName = firstName;
                LastName = lastName;

                if (hobbies == null)
                    hobbies = new List<string>();
                Hobbies = hobbies.AsReadOnly();
            }

            public bool Equals(PersonWithHobbies? p)
            {
                if (p == null)
                    return false;

                return FirstName.Equals(p.FirstName) && LastName.Equals(p.LastName) && Hobbies.Count.Equals(p.Hobbies.Count);
            }

            public override int GetHashCode() => HashCode.Combine(FirstName, LastName, Hobbies.Count);
        }

        [Fact]
        public void HobbiesAreReadOnly()
        {
            var p1 = new PersonWithHobbies("wonder", "woman", new List<string> { "hiking", "biking" });

            //// no allowed since it is readonly
            //p1.Hobbies.Add...
            Assert.Equal(2, p1.Hobbies.Count);
            Assert.IsAssignableFrom<IReadOnlyCollection<string>>(p1.Hobbies);
        }

        [Fact]
        public void RecordWithRefTypeTest1()
        {
            var p1 = new PersonWithHobbies("captain", "america", new List<string> { "hiking", "biking" });
            var p2 = new PersonWithHobbies("captain", "america", new List<string> { "hiking", "fishing" });

            var p1EqualsP2 = p1.Equals(p2);

            // they are equal - cause we have Equal override
            Assert.True(p1EqualsP2);
            Assert.False(ReferenceEquals(p1, p2));
        }

        [Fact]
        public void Got_iEquatable_out_of_the_box()
        {
            var p1 = new PersonWithHobbies("bruce", "banner", new List<string> { "hiking", "fishing" });

            var equatableP1 = p1 as IEquatable<PersonWithHobbies>;

            // record automatically implements IEquatable interface
            Assert.NotNull(equatableP1);
            Assert.IsAssignableFrom<IEquatable<PersonWithHobbies>>(p1);
        }

        [Fact]
        public void Got_equal_equal_out_of_the_box()
        {
            var p1 = new PersonWithHobbies("bruce", "banner", new List<string> { "hunting", "fishing" });
            var p2 = new PersonWithHobbies("bruce", "banner", new List<string> { "hunting", "fishing" });

            var p1EqualsP2 = p1 == p2;

            Assert.True(p1EqualsP2);
            // p1 and p2 are two different object in memory
            Assert.False(ReferenceEquals(p1, p2));
        }
    }
}