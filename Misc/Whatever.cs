namespace Misc;

public class Whatever
{
    private class SomePersonClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    private struct SomePersonStruct
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    private void OutPersonClass(out SomePersonClass p)
    {
        p = new SomePersonClass
        {
            Id = 1,
            Name = "one"
        };
    }

    private void OutPersonStruct(out SomePersonStruct p)
    {
        p = new SomePersonStruct
        {
            Id = 2,
            Name = "two"
        };
    }

    [Fact]
    public void OutExperiment()
    {
        SomePersonClass somePersonClass;
        OutPersonClass(out somePersonClass);
        Assert.NotNull(somePersonClass);
        Assert.Equal("one", somePersonClass.Name);

        SomePersonStruct somePersonStruct;
        OutPersonStruct(out somePersonStruct);
        Assert.Equal("two", somePersonStruct.Name);
    }
}
