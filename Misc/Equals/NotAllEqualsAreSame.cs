namespace Misc.Equals;

public class NotAllEqualsAreSame
{
    private readonly ITestOutputHelper testOutputHelper;

    public NotAllEqualsAreSame(ITestOutputHelper testOutputHelper)
    {
        this.testOutputHelper = testOutputHelper;
    }

    /// <summary>
    /// when target type support == and != use it
    /// it is static, ready to use and much faster
    /// than using virtual Equals - but virtual Equal has one
    /// upper hand - this has to work where expected
    /// </summary>
    [Fact]
    public void T01WhenPossibleUse_EqualEqual()
    {
        var x = 5;
        var y = 5;

        Assert.True(x == y);
        Assert.False(x != y);

        // remember equality works both ways
        Assert.True(y == x);
        Assert.False(y != x);
    }

    /// <summary>
    /// event with support for == and !=; have to keep
    /// state of the objects in mind
    /// </summary>
    [Fact]
    public void T02Careful_NotAll_EqualEqual_WorksTheSame()
    {
        var x = 5;
        var y = 5;
        Assert.True(x == y);

        var oX = (object)x;
        var oY = (object)y;
        // reason for not equal - once cast into object
        // it starts doing ref. equal; and ofcouse these
        // are two different objects on the heap
        Assert.False(oX == oY);

        // this is where virtual Equals comes to the rescue
        // remember - virtual methods have additional costs
        // not as fast as static == operator
        Assert.True(oX.Equals(oY));
    }

#pragma warning disable CS1718 // Comparison made to same variable
    /// <summary>
    /// common sense - everything should be equal to itself
    /// but there are few exceptions
    /// </summary>
    [Fact]
    public void T03EverythingShouldBe_EqualToItself()
    {
        var d = 5.9d; // d is a double
        // this is expected and no surprise
        Assert.True(d == d);

        // but there are exception - NaN (not a number)
        var dnAn = double.NaN;
        // this is false - NaN is like a special case
        Assert.False(dnAn == dnAn);
        Assert.False(dnAn == double.NaN);

        // this is where virtual Equals comes to the rescue
        Assert.True(dnAn.Equals(dnAn));
        // another way to check if a double is NaN
        Assert.True(double.IsNaN(dnAn));

        // why does == fails but Equals succeeds???
        // NaN is a life's mystery
        // look at decompiled version for the definition
        // this is what defines NaN
        var d2 = (double)0.0 / (double)0.0;
        Assert.True(double.IsNaN(d2));
    }
#pragma warning restore CS1718 // Comparison made to same variable

    [Fact]
    public void T04StringIsSpecial_TheyShowInterestingBehavior()
    {
        // two different objects on the heap
        var s1 = "test";
        var s2 = "test";

        // this is expected
        Assert.True(s1 == s2);
        // this is also expected
        Assert.True(s1.Equals(s2));

        // strings are ref. type - they get allocated on the heap
        // guess what's the result of this
        testOutputHelper.WriteLine($"ReferenceEquals(s1, s2): {ReferenceEquals(s1, s2)}");

        // how about this one?
        var oS1 = (object)s1;
        var oS2 = (object)s2;
        testOutputHelper.WriteLine($"ReferenceEquals(oS1, oS2): {ReferenceEquals(oS1, oS2)}");

        // how about this one?
        testOutputHelper.WriteLine($"ReferenceEquals(s1, oS2): {ReferenceEquals(s1, oS2)}");
    }

    [Fact]
    public void T05StringIsSpecial_TheyShowInterestingBehavior()
    {
        // again - start with two different objects on the heap
        // but this time initialized differently
        var s1 = "test";
        var s2 = "test1".Substring(0, 4);

        // like above - these two are no surprise
        Assert.True(s1 == s2);
        Assert.True(s1.Equals(s2));

        // how about ref. equal
        testOutputHelper.WriteLine($"ReferenceEquals(s1, s2): {ReferenceEquals(s1, s2)}");

        var oS2 = (object)s2;
        // how about now?
        testOutputHelper.WriteLine($"ReferenceEquals(s2, oS2): {ReferenceEquals(s2, oS2)}");
        // this is expected
        testOutputHelper.WriteLine($"s2 == oS2 gives us => {s2 == oS2}");

        testOutputHelper.WriteLine("we know s1 == s2 is true, and s1 == oS2 is true");
        testOutputHelper.WriteLine("  because of transitive property we should get s1 == oS2");
        // how about his one?
        testOutputHelper.WriteLine($"s1 == oS2 gives us => {s1 == oS2}");
    }
}
