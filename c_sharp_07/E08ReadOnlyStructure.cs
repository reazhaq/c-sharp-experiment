using Xunit;
using Xunit.Abstractions;

namespace c_sharp_07
{
    public class E08ReadOnlyStructure
    {
        private readonly ITestOutputHelper testOutputHelper;

        public E08ReadOnlyStructure(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

        private readonly struct ReadOnlyPersonStruct
        {
            // field has to be "readonly"
            public readonly int Id;
            // readonly can't have any setter - not even a private one
            public string Name { get; }

            public ReadOnlyPersonStruct(int id, string name)
            {
                Id = id;
                Name = name;
            }

            //public void ChangeId(int id) => Id = id; // Error - can't be assigned, it is readonly
            public override string ToString() => Name;
        }

        private struct RegularPersonStruct
        {
            public readonly int Id;
            // regular structure can do private setter
            public string Name { get; private set; }

            public RegularPersonStruct(int id, string name)
            {
                Id = id;
                Name = name;
            }

            //public void ChangeId(int id) => Id = id; // Error - can't be assigned, it is readonly

            public void ChangeName(string newName) => Name = newName; // this is good - calling private setter
            public override string ToString() => Name;
        }

        private struct RegularButReadOnlyPerson
        {
            // field has to be "readonly"
            public readonly int Id;
            // readonly since no setter
            public string Name { get; }

            public RegularButReadOnlyPerson(int id, string name)
            {
                Id = id;
                Name = name;
            }

            //public void ChangeId(int id) => Id = id; // Error - can't be assigned, it is readonly
            //public void ChangeName(string name) => Name = name; // Error - can't be assigned, it is readonly
            override public string ToString() => Name;
        }

        private void PrintReadOnlyPersonStruct(in ReadOnlyPersonStruct p)
        {
            testOutputHelper.WriteLine($"read-only-person-struct p.ToString: {p}");
        }

        [Fact]
        public void PrintReadOnlyPersonStructTest()
        {
            var p = new ReadOnlyPersonStruct(1, "bob");

            PrintReadOnlyPersonStruct(p);
        }
    }
}
