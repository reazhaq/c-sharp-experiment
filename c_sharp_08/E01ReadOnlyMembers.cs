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

        private struct PersonStruct
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public void ChangeName(string newName)
            {
                Name = newName;
            }

            public override string ToString() => $"id: {Id} - name: {Name}";
        }

        private struct PersonStructWithReadOnlyMemeber
        {
            // auto property getter is readonly
            public int Id { get; set; }
            public string Name { get; set; }

            //public readonly void ChangeName(string newName)
            //{
            //    Name = newName; // Error - method signature is readonly - meaning inside nothing can't be changed
            //}

            public readonly override string ToString() => $"id: {Id} - name: {Name}";
        }

        private void PrintPersonStructure(in PersonStruct p)
        {
            testOutputHelper.WriteLine(p.ToString());
        }

        private void PrintPersonStructWithReadOnlyMemeber(in PersonStructWithReadOnlyMemeber p)
        {
            testOutputHelper.WriteLine(p.ToString());
        }

        [Fact]
        public void Blah1()
        {
            var p1 = new PersonStruct { Id = 1, Name = "one" };
            PrintPersonStructure(p1);

            var p2 = new PersonStructWithReadOnlyMemeber { Id = 2, Name = "two" };
            PrintPersonStructWithReadOnlyMemeber(p2);
        }

        private struct PersonStructWithRefReadOnlyReturn
        {
            public int Id { get; set; } = 0;
            public string Name { get; set; } = "unknown";

            private static PersonStructWithRefReadOnlyReturn unknownPerson = new PersonStructWithRefReadOnlyReturn();

            //// this is not safe - cause caller can make changes to "unknownPerson"
            // public ref PersonStructWithRefReadOnlyReturn UnknownPerson => ref unknownPerson;

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

            // this gives you direct reference to the original instance
            // and compiler guarantees that it can't be used to make any changes to the instance
            ref readonly var directRefToUnknownPerson = ref PersonStructWithRefReadOnlyReturn.UnknownPerson;
            //directRefToUnknownPerson.Name = "try to change - but error";
        }
    }
}
