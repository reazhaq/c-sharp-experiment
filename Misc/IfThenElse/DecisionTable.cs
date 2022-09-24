namespace Misc.IfThenElse;

public enum TravelTypes
{
    Student,
    Teacher,
    Staff
}

public enum TravelReasons
{
    FieldTrip,
    Seminar,
    Personal,
    Picnic
}

public enum Destinations
{
    InState,
    OutOfState,
    International
}

public class DecisionTable
{
    private readonly ITestOutputHelper testOutputHelper;

    public DecisionTable(ITestOutputHelper testOutputHelper)
    {
        this.testOutputHelper = testOutputHelper;
    }

    /// <summary>
    /// | group travel | travel type | travel reason | destination   | expense account|
    /// | ------------ | ----------- | ------------- | ------------- | -------------- |
    /// | yes          | student     | field trip    | in-state      | 0001           |
    /// | yes          | student     | field trip    | out-of-state  | 0002           |
    /// | yes          | student     | field trip    | international | 0003           |
    /// | yes          | teacher     | seminar       | in-state      | 0003           |
    /// | yes          | teacher     | personal      | international | 0004           |
    /// </summary>
    public string FindExpenseAccount(bool grTravel, TravelTypes tt, TravelReasons tr, Destinations d) =>
        (grTravel, tt, tr, d) switch
        {
            (true, TravelTypes.Student, TravelReasons.FieldTrip, Destinations.InState      ) => "0001",
            (true, TravelTypes.Student, TravelReasons.FieldTrip, Destinations.OutOfState   ) => "0002",
            (true, TravelTypes.Student, TravelReasons.FieldTrip, Destinations.International) => "0003",
            (true, TravelTypes.Teacher, TravelReasons.Seminar  , Destinations.InState      ) => "0003",
            (true, TravelTypes.Teacher, TravelReasons.Personal , Destinations.International) => "0004",
            _ => throw new NotImplementedException(),
        };

    [Theory]
    [InlineData(true, TravelTypes.Student, TravelReasons.FieldTrip, Destinations.InState, "0001")]
    [InlineData(true, TravelTypes.Student, TravelReasons.FieldTrip, Destinations.OutOfState, "0002")]
    [InlineData(true, TravelTypes.Student, TravelReasons.FieldTrip, Destinations.International, "0003")]
    [InlineData(true, TravelTypes.Teacher, TravelReasons.Seminar, Destinations.InState, "0003")]
    [InlineData(true, TravelTypes.Teacher, TravelReasons.Personal, Destinations.InState, "0004")]
    public void FindExpenseAccountTests(bool grTravel, TravelTypes tt, TravelReasons tr, Destinations d, string expectedReturn)
    {
        var expenseAccount = FindExpenseAccount(grTravel, tt, tr, d);

        Assert.Equal(expectedReturn, expenseAccount);
    }
}
