namespace Misc.IfThenElse;

public class TooManyIfStatements
{
    private readonly ITestOutputHelper testOutputHelper;

    public TooManyIfStatements(ITestOutputHelper testOutputHelper)
    {
        this.testOutputHelper = testOutputHelper;
    }

    class Transportation
    {
        public string Name { get; protected set; }
        public Transportation() { Name = "transport"; }
    }
    class Car : Transportation { public Car() { Name = "car"; } }
    class Motorcycle : Transportation { public Motorcycle() { Name = "motorcycle"; } }
    class Bus : Transportation { public Bus() { Name = "bus"; } }
    class Truck : Transportation { public Truck() { Name = "truck"; } }
    class Train : Transportation { public Train() { Name = "train"; } }
    class Plane : Transportation { public Plane() { Name = "plane"; } }
    class Rocket : Transportation { public Rocket() { Name = "rocket"; } }

    /// <summary>
    /// this is a nasty code-smell; lots of nested if-else
    /// even if we replace it with if - return; or with a switch statement still
    ///   - lots of condition to execute
    ///   - and it becomes a maintenance problem
    ///     - not very read friendly
    ///     - and easy to make mistakes as we add more
    /// </summary>
    /// <param name="name">what to create</param>
    /// <returns></returns>
    private Transportation CreateTransportation(string name)
    {
        Transportation transportation = null;
        if (!string.IsNullOrEmpty(name))
        {
            if (name.Equals("transport"))
                transportation = new Transportation();
            else if (name.Equals("car"))
                transportation = new Car();
            else if (name.Equals("motorcycle"))
                transportation = new Motorcycle();
            else if (name.Equals("bus"))
                transportation = new Bus();
            else if (name.Equals("truck"))
                transportation = new Truck();
            else if (name.Equals("train"))
                transportation = new Train();
            else if (name.Equals("plane"))
                transportation = new Plane();
            else if (name.Equals("rocket"))
                transportation = new Rocket();
        }

        return transportation;
    }

    [Theory]
    [InlineData("transport", typeof(Transportation))]
    [InlineData("car", typeof(Car))]
    [InlineData("motorcycle", typeof(Motorcycle))]
    [InlineData("bus", typeof(Bus))]
    [InlineData("truck", typeof(Truck))]
    [InlineData("train", typeof(Train))]
    [InlineData("plane", typeof(Plane))]
    [InlineData("rocket", typeof(Rocket))]
    public void CreateTransportationUsing_CreateTransportation(string name, Type expectedTransportType)
    {
        var transportation = CreateTransportation(name);

        testOutputHelper.WriteLine($"{name} => created transport with name: {transportation.Name}");
        Assert.NotNull(transportation);
        Assert.Equal(expectedTransportType, transportation.GetType());
    }

    ////////////////////////////////////////
    /// <summary>
    /// better way to do this - forget about if-else
    /// start with a factory mapping in a easy to lookup data structure 
    /// </summary>

    private Dictionary<string, Func<Transportation>> TransportationCreatorMapping = new Dictionary<string, Func<Transportation>>
    {
        { "transport", () => new Transportation() },
        { "car", () => new Car() },
        { "motorcycle", () => new Motorcycle() },
        { "bus", () => new Bus() },
        { "truck", () => new Truck() },
        { "train", () => new Train() },
        { "plane", () => new Plane() },
        { "rocket", () => new Rocket() },
    };

    private Transportation CreateTransportationImproved(string name)
    {
        if (!string.IsNullOrWhiteSpace(name))
        {
            if (TransportationCreatorMapping.TryGetValue(name, out var transportationCreator))
                return transportationCreator();
        }

        return null;
    }

    [Theory]
    [InlineData("transport", typeof(Transportation))]
    [InlineData("car", typeof(Car))]
    [InlineData("motorcycle", typeof(Motorcycle))]
    [InlineData("bus", typeof(Bus))]
    [InlineData("truck", typeof(Truck))]
    [InlineData("train", typeof(Train))]
    [InlineData("plane", typeof(Plane))]
    [InlineData("rocket", typeof(Rocket))]
    public void CreateTransportationUsing_CreateTransportationImproved(string name, Type expectedTransportType)
    {
        var transportation = CreateTransportationImproved(name);

        testOutputHelper.WriteLine($"{name} => created transport with name: {transportation.Name}");
        Assert.NotNull(transportation);
        Assert.Equal(expectedTransportType, transportation.GetType());
    }
}
