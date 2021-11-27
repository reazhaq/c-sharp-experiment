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

        private readonly struct ReadonlyStructPerson
        {
            // field has to be "readonly"
            public readonly int Id;
            // implict readonly property - since there is no "set"
            public string Name { get; }

            public ReadonlyStructPerson(int id, string name)
            {
                Id = id;
                Name = name;
            }

            //public void ChangeId(int id)
            //{
            //    Id = id; // Error - can't be assigned, it is readonly
            //}
        }

        private struct RegularStructPerson
        {
            public readonly int Id;
            // regular structure can do private setter
            public string Name { get; private set; }

            public RegularStructPerson(int id, string name)
            {
                Id = id;
                Name = name;
            }

            //public void ChangeId(int id)
            //{
            //    Id = id; // Error - can't be assigned, it is readonly
            //}

            public void ChangeName(string newName)
            {
                Name = newName; // this is good - calling private setter
            }
        }
    }
}
