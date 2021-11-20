namespace c_sharp_10;

public class E01Structure
{
    private readonly ITestOutputHelper testOutputHelper;

    public E01Structure(ITestOutputHelper testOutputHelper)
    {
        this.testOutputHelper = testOutputHelper;
    }

    private struct Person
    {
        public Person()
        {
            Name = "unknown";
            Age = int.MinValue;
            Weight = double.NaN;
        }

        public string Name { get; set; }
        public int Age { get; set; }
        public double Weight { get; set; }

        public override string ToString() =>
            $"person \"{Name ?? "null-string"}\" is \"{Age}\" years old, weighing \"{Weight:0.00}\" lbs";
    }

    [Fact]
    public void Structure_can_have_parameterless_constructor()
    {
        var p = new Person();

        testOutputHelper.WriteLine(p.ToString());
        Assert.Equal("unknown", p.Name);
        Assert.Equal(int.MinValue, p.Age);
        Assert.Equal(double.NaN, p.Weight);
    }

    [Fact]
    public void Structure_still_got_default_parameterless_constructor()
    {
        var p = default(Person);

        testOutputHelper.WriteLine(p.ToString());
        // default calls built-in parameterless constructor
        // which sets every structure member to types' defaults
        Assert.Null(p.Name);
        Assert.Equal(default(int), p.Age);
        Assert.Equal(default(double), p.Weight);
    }

    [Fact]
    public void Array_of_structure_uses_default_constructor()
    {
        var persons = new Person[2];

        Assert.Equal(2, persons.Length);
        // first element is set to default
        Assert.Null(persons[0].Name);
        Assert.Equal(default(int), persons[0].Age);
        Assert.Equal(default(double), persons[0].Weight);
        // so is the 2nd element
        Assert.Null(persons[1].Name);
        Assert.Equal(default(int), persons[1].Age);
        Assert.Equal(default(double), persons[1].Weight);
    }
}
