using Xunit.Abstractions;
using Xunit;
using System;
using System.Collections.Generic;
using model.Geo;

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
        ////// this is same as Person - but much more
        //private class Person2
        //{
        //    public string FirstName { get; }
        //    public string LastName { get; }
        //}

        [Fact]
        public void Record_is_a_ref_type()
        {
            var p = new Person("jon", "doe");

            Assert.False(p.GetType().IsValueType);
            Assert.True(p.GetType().IsClass);
        }

        [Fact]
        public void Record_is_immutable_by_default()
        {
            var p1 = new Person("jon", "doe");

            Assert.Equal("jon", p1.FirstName);
            Assert.Equal("doe", p1.LastName);

            //// this is not allowed - because properties is readonly
            //p1.FirstName = "bobby";

            // first keep a copy of p1 ref - so that we can do ref equal comparison
            var p1OldRef = p1;
            
            //// this is allowed - and the new p1 is different object from previous one
            p1 = p1 with { FirstName = "bobby" };
            
            // their refernce equality fails
            Assert.False(object.ReferenceEquals(p1OldRef, p1));

            //// what if I assign back the same first name
            p1 = p1 with { FirstName = "jon" };

            // they are two different object
            Assert.False(object.ReferenceEquals(p1OldRef, p1));
            // but value equal
            Assert.True(p1.Equals(p1OldRef));
        }

        [Fact]
        public void Record_got_iEquatable_out_of_the_box()
        {
            var p1 = new Person("jon", "doe");

            var equatableP1 = p1 as IEquatable<Person>;

            // record automatically implements IEquatable interface
            Assert.NotNull(equatableP1);
            Assert.IsAssignableFrom<IEquatable<Person>>(p1);
        }

        [Fact]
        public void Record_got_equals_out_of_the_box()
        {
            var p1 = new Person("jon", "doe");
            var p2 = new Person("jon", "doe");

            // this call IEquatable.Equals (not object.Equals)
            var p1EqualsP2 = p1.Equals(p2);

            // p1 and p2 are equal - cause their properties are equal
            Assert.True(p1EqualsP2);
            // p1 and p2 are two different object in memory
            Assert.False(object.ReferenceEquals(p1, p2));
        }

        [Fact]
        public void Record_got_equal_equal_out_of_the_box()
        {
            var p1 = new Person("jon", "doe");
            var p2 = new Person("jon", "doe");

            // this call IEquatable.Equals (not object.Equals)
            var p1EqualsP2 = p1 == p2;

            // p1 and p2 are equal - cause their properties are equal
            Assert.True(p1EqualsP2);
            // p1 and p2 are two different object in memory
            Assert.False(object.ReferenceEquals(p1, p2));
        }

        [Fact]
        public void How_about_hash_code_out_of_the_box()
        {
            var p1 = new Person("jon", "doe");
            var p2 = new Person("jon", "doe");

            var h1 = p1.GetHashCode();
            var h2 = p2.GetHashCode();

            // h1 and h2 are equal - cause their properties are equal
            Assert.True(h1 == h2);
            // p1 and p2 are two different object in memory
            Assert.False(object.ReferenceEquals(p1, p2));
        }

        // derived class with other value type or immutable type
        private record PersonWithAge(string FirstName, string LastName, int age) : Person(FirstName, LastName);

        [Fact]
        public void Derived_record_got_equality_working_when_we_have_value_type_memebers_only()
        {
            var p1 = new PersonWithAge("jon", "doe", 99);
            var p2 = new PersonWithAge("jon", "doe", 99);

            var p1EqualsP2 = p1.Equals(p2);

            Assert.True(p1EqualsP2);
            Assert.False(object.ReferenceEquals(p1, p2));
        }

        private record Employee(Person person, string title);

        [Fact]
        public void Record_with_record_member_got_equality_working()
        {
            var e1 = new Employee(new Person("jon", "doe"), "boss");
            var e2 = new Employee(new Person("jon", "doe"), "boss");

            var e1EqualsE2 = e1.Equals(e2);

            Assert.True(e1EqualsE2);
        }

        // this is interesting - cause List is not immutable, so two records with two different list
        // doesn't work. but two records with the same list object works
        private record PersonWithHobbies(string FirstName, string LastName, List<string> Hobbies) : Person(FirstName, LastName);

        [Fact]
        public void Derived_record_equality_not_working_when_a_member_is_another_refType()
        {
            var p1 = new PersonWithHobbies("jon", "doe", new List<string> { "hiking", "biking" });
            var p2 = new PersonWithHobbies("jon", "doe", new List<string> { "hiking", "biking" });

            var p1EqualsP2 = p1.Equals(p2);

            // they are not equal - cause List is a ref type
            Assert.False(p1EqualsP2);
            Assert.False(object.ReferenceEquals(p1, p2));
        }

        [Fact]
        public void Derived_record_equality_working_when_same_refObject_is_used()
        {
            var hobbies = new List<string> { "hiking", "biking" };
            var p1 = new PersonWithHobbies("jon", "doe", hobbies);
            var p2 = new PersonWithHobbies("jon", "doe", hobbies);

            var p1EqualsP2 = p1.Equals(p2);

            // they are equal - cause same List is used
            Assert.True(p1EqualsP2);
            Assert.False(object.ReferenceEquals(p1, p2));
        }

        private record PersonWithLocation(string FirstName, string LastName, GeoPoint geoPoint) : Person(FirstName, LastName);

        [Fact]
        public void Derived_record_with_user_defined_ref_type_equality()
        {
            var p1 = new PersonWithLocation("jon", "doe", new GeoPoint(1, 1));
            var p2 = new PersonWithLocation("jon", "doe", new GeoPoint(1, 1));

            var p1EqualsP2 = p1.Equals(p2);

            Assert.False(p1EqualsP2);
            Assert.False(object.ReferenceEquals(p1, p2));
        }

        private record PersonWithImmutableLocation(string FirstName, string LastName, GeoPointImmutable GeoPointImmutable) : Person(FirstName, LastName);

        [Fact]
        public void Derived_record_with_immutable_user_defined_ref_type_equality()
        {
            var p1 = new PersonWithImmutableLocation("jon", "doe", new GeoPointImmutable(1, 1));
            var p2 = new PersonWithImmutableLocation("jon", "doe", new GeoPointImmutable(1, 1));

            var p1EqualsP2 = p1.Equals(p2);

            Assert.True(p1EqualsP2);
            Assert.False(object.ReferenceEquals(p1, p2));
        }
    }
}
