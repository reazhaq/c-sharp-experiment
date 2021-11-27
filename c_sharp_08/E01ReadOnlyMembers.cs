using Xunit;
using Xunit.Abstractions;

namespace c_sharp_08
{
    public class E01ReadOnlyMembers
    {
        private readonly ITestOutputHelper testOutputHelper;

        public E01ReadOnlyMembers(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

        private struct PersonStructWithReadOnlyMemeber
        {
            // guarantees that calling "getter" not changing any value
            public int Id { readonly get; private set; }
            public string Name { get; private set; }

            //public readonly void ChangeName(string newName)
            //{
            //    Name = newName; // Error - method signature is readonly - meaning inside nothing can't be changed
            //}
        }

        private struct PersonStructWithRefReadOnlyReturn
        {
            public int Id { get; set; } = 0;
            public string Name { get; set; } = "unknown";

            private static PersonStructWithRefReadOnlyReturn unknownPerson = new PersonStructWithRefReadOnlyReturn();

            //// this is not safe - cause caller can make changes to "unknownPerson"
            //public ref PersonStructWithRefReadOnlyReturn UnknownPerson => ref unknownPerson;

            // caller using this can't make any changes - guaranteed
            public static ref readonly PersonStructWithRefReadOnlyReturn UnknownPerson => ref unknownPerson;
        }

        [Fact]
        public void RefReadOnly_gives_you_direct_access()
        {
            // this statement makes a copy of UnknownPerson instance
            var copyOfUnknowPerson = PersonStructWithRefReadOnlyReturn.UnknownPerson;
            copyOfUnknowPerson.Name = "changed name";
            Assert.Equal("unknown", PersonStructWithRefReadOnlyReturn.UnknownPerson.Name);
            Assert.Equal("changed name", copyOfUnknowPerson.Name);

            // this gives you direct reference to the original instance memory
            // and compiler guarantees that it can't be used to make any changes to the instance
            ref readonly var directRefToUnknownPerson = ref PersonStructWithRefReadOnlyReturn.UnknownPerson;
            // directRefToUnknownPerson.Name = "try to change - but error";
        }
    }
}
