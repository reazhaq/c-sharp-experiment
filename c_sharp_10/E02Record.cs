namespace c_sharp_10;

public class E02Record
{
    private readonly ITestOutputHelper testOutputHelper;

    public E02Record(ITestOutputHelper testOutputHelper)
    {
        this.testOutputHelper = testOutputHelper;
    }

    private record class PersonClass(string firstName, string lastName);

    [Fact]
    public void Record_class_is_ref_type()
    {
        var p = new PersonClass("jon", "doe");

        Assert.False(p.GetType().IsValueType);
        Assert.True(p.GetType().IsClass);
    }

    private record struct PersonStruct(string firstName, string lastName);

    [Fact]
    public void Record_struct_is_value_type()
    {
        var d = new PersonStruct("jon", "doe");

        Assert.True(d.GetType().IsValueType);
        Assert.False(d.GetType().IsClass);
    }
}
