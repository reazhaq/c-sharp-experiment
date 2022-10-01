namespace Misc.Equals;

public class CorrectWayToOverwriteEquals
{
    private readonly ITestOutputHelper testOutputHelper;
    public CorrectWayToOverwriteEquals(ITestOutputHelper testOutputHelper)
    {
        this.testOutputHelper = testOutputHelper;
    }

    class Person : IEquatable<Person>
    {
        private readonly ITestOutputHelper testOutputHelper;
        public int Id { get; set; }
        public string Name { get; set; }

        public Person(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

        public bool Equals(Person other)
        {
            testOutputHelper?.WriteLine("Equals(Person other) is called");
            if (other is null) return false;
            return this == other;
        }

        public override bool Equals(object obj)
        {
            testOutputHelper?.WriteLine("Equals(object obj) is called");
            return Equals(obj as Person);
        }

        public override int GetHashCode() =>
            HashCode.Combine(Id, Name);

        public static bool operator ==(Person left, Person right)
        {
            if (ReferenceEquals(left, right)) return true;
            if (left is null && right is null) return true;
            if (left is not null && right is not null)
            {
                return left.Id == right.Id && left.Name == right.Name;
            }

            return false;
        }

        public static bool operator !=(Person left, Person right) => !(left == right);
    }

    public bool AreObjectsEqual<T>(T left, T right)
    {
        testOutputHelper.WriteLine("executing AreObjectsEqual<T>(T left, T right)");
        if (left is null && right is null) return true;

        if (left is not null)
            return left.Equals(right);

        if (right is not null)
            return right.Equals(left);

        return false;
    }

    public bool AreObjectsEqual2<T>(T left, T right)
    {
        testOutputHelper.WriteLine("executing AreObjectsEqual2<T>(T left, T right)");
        var equalityComparer = EqualityComparer<T>.Default;
        return equalityComparer.Equals(left, right);
    }

    [Fact]
    public void T01Use_AreObjectsEqual_ForIronManAndDrStrange()
    {
        var p1 = new Person(testOutputHelper) { Id = 1, Name = "iron man" };
        var p2 = new Person(testOutputHelper) { Id = 2, Name = "dr. strange" };

        var areEqual = AreObjectsEqual(p1, p2);

        Assert.False(areEqual);
    }

    [Fact]
    public void T02Use_AreObjectsEqual2_ForIronManAndDrStrange()
    {
        var p1 = new Person(testOutputHelper) { Id = 1, Name = "iron man" };
        var p2 = new Person(testOutputHelper) { Id = 2, Name = "dr. strange" };

        var areEqual = AreObjectsEqual2(p1, p2);

        Assert.False(areEqual);
    }

    [Fact]
    public void T03Use_AreObjectsEqual_BothNullPerson()
    {
        var p1 = (Person)null;
        var p2 = (Person)null;

        var areEqual = AreObjectsEqual(p1, p2);

        Assert.True(areEqual);
    }

    [Fact]
    public void T04Use_AreObjectsEqual2_BothNullPerson()
    {
        var p1 = (Person)null;
        var p2 = (Person)null;

        var areEqual = AreObjectsEqual2(p1, p2);

        Assert.True(areEqual);
    }

    [Fact]
    public void T05Use_AreObjectsEqual_OneNullPerson()
    {
        var p1 = (Person)null;
        var p2 = new Person(testOutputHelper);

        var areEqual = AreObjectsEqual(p1, p2);

        Assert.False(areEqual);
    }

    [Fact]
    public void T06Use_AreObjectsEqual2_OneNullPerson()
    {
        var p1 = (Person)null;
        var p2 = new Person(testOutputHelper);

        var areEqual = AreObjectsEqual2(p1, p2);

        Assert.False(areEqual);
    }

    [Fact]
    public void T07Use_AreObjectsEqual_OtherNullPerson()
    {
        var p1 = new Person(testOutputHelper);
        var p2 = (Person)null;

        var areEqual = AreObjectsEqual(p1, p2);

        Assert.False(areEqual);
    }

    [Fact]
    public void T08Use_AreObjectsEqual2_OtherNullPerson()
    {
        var p1 = new Person(testOutputHelper);
        var p2 = (Person)null;

        var areEqual = AreObjectsEqual2(p1, p2);

        Assert.False(areEqual);
    }
}
