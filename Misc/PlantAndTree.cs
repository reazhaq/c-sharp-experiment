namespace Misc;

public class Plant
{
    public string WhoAmI() => "I am a plant";
    public virtual string WhoAmI2() => "I am a plant";
}

public class Tree : Plant
{
    public new string WhoAmI() => "I am a Tree";
    public override string WhoAmI2() => "I am a Tree";
}

public class PlantAndTreeTest
{
    private readonly ITestOutputHelper testOutputHelper;

    public PlantAndTreeTest(ITestOutputHelper testOutputHelper)
    {
        this.testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void PlantOrTree()
    {
        Plant p = new Tree();
        testOutputHelper.WriteLine(p.WhoAmI());

        Plant p2 = new Tree();
        testOutputHelper.WriteLine(p2.WhoAmI2());
    }

    [Fact]
    public void IntIncrement()
    {
        var myInt = 5;
        testOutputHelper.WriteLine($"{myInt++}");
        myInt = 5;
        testOutputHelper.WriteLine($"{++myInt}");
        myInt = 5;
        myInt += 1;
        testOutputHelper.WriteLine($"{myInt:00}");

        var num = 5.4;
        var roundedNumber = (int)(num + 0.5);
        testOutputHelper.WriteLine($"{num} {roundedNumber}");
    }
}



