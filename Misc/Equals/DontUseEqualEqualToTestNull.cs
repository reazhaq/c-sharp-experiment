namespace Misc.Equals;

public class DontUseEqualEqualToTestNull
{
    class Person
    {
        public string Name { get; set; }

        public static bool operator ==(Person left, Person right) => false;

        public static bool operator !=(Person left, Person right) => !(left == right);
    }

    [Fact]
    public void EqualEqualNull_IsNotReliable()
    {
        var n = (Person)null;

        // n == null gives us false because of operator override
        Assert.False(n == null);

        // to guarantee null check use is
        Assert.True(n is null);
    }
}
