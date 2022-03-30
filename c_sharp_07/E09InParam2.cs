using Xunit;
using Xunit.Abstractions;

namespace c_sharp_07;

public class E09InParam2
{
    private readonly ITestOutputHelper testOutputHelper;

    public E09InParam2(ITestOutputHelper testOutputHelper)
    {
        this.testOutputHelper = testOutputHelper;
    }
    private struct PersonStruct2
    {
        public string Name { get; set; }

        public void ChangeName(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return $"Name is: {Name}";
        }
    }


    private void ChangeName(in PersonStruct2 p)
    {
        testOutputHelper.WriteLine($"before [in] p.ChangeName - Name: {p.Name}");
        p.ChangeName("spider man");
        testOutputHelper.WriteLine($"after [in] p.ChangeName - Name: {p.Name}");
    }

    private void ChangeName(PersonStruct2 p)
    {
        testOutputHelper.WriteLine($"before p.ChangeName - Name: {p.Name}");
        p.ChangeName("spider man");
        testOutputHelper.WriteLine($"after p.ChangeName - Name: {p.Name}");
    }

    [Fact]
    public void InParamDoesnotPreventCallingMethodsThatChangesProperties()
    {
        var p1 = new PersonStruct2 { Name = "batman" };
        testOutputHelper.WriteLine($"before calling [in] ChangeName - Name: {p1.Name}");
        ChangeName(in p1);
        testOutputHelper.WriteLine($"after calling [in] ChangeName - Name: {p1.Name}");
    }

    [Fact]
    public void InParamDoesnotPreventCallingMethodsThatChangesProperties2()
    {
        var p1 = new PersonStruct2 { Name = "batman" };
        testOutputHelper.WriteLine($"before calling ChangeName - Name: {p1.Name}");
        ChangeName(p1);
        testOutputHelper.WriteLine($"after calling ChangeName - Name: {p1.Name}");
    }
}
