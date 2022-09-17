namespace Misc.EnumMisc;

public enum CustomerTypes
{
    Guest = 0,
    NewCustomer,
    Prime,
    Elite,
}

public class Customer
{
    public CustomerTypes customerType { get; }
    public int discount { get; set; }
    public Customer(CustomerTypes customerType) => this.customerType = customerType;
}

public class Guest : Customer
{
    public Guest() : base(CustomerTypes.Guest) { }
}

public class NewCustomer : Customer
{
    public NewCustomer() : base(CustomerTypes.NewCustomer) { }
}

public class Prime : Customer
{
    public Prime() : base(CustomerTypes.Prime) { }
}

public class Elite : Customer
{
    public Elite() : base(CustomerTypes.Elite) { }
}

public class CodeSmellUsingEnum
{
    private readonly ITestOutputHelper testOutputHelper;

    public CodeSmellUsingEnum(ITestOutputHelper testOutputHelper)
    {
        this.testOutputHelper = testOutputHelper;
    }

    /// <summary>
    /// this is a code smell:
    ///   this section have very direct knowledge of enums
    ///   similar code can show up in multiple places - like another method for shipping charge
    /// </summary>
    /// <param name="customer"></param>
    /// <exception cref="ArgumentException"></exception>
    private void CalculateDiscountPercentage(Customer customer)
    {
        if (customer == null || !Enum.IsDefined(customer.customerType))
            throw new ArgumentException("customer can't be null, or have bad type");

        if (customer.customerType == CustomerTypes.Guest)
            customer.discount = 0;
        else if (customer.customerType == CustomerTypes.NewCustomer)
            customer.discount = 5;
        else if (customer.customerType == CustomerTypes.Prime)
            customer.discount = 10;
        else if (customer.customerType == CustomerTypes.Elite)
            customer.discount = 15;
        else
            customer.discount = 0;
    }

    [Fact]
    public void CalculateDiscountPercentage_SetDiscount_forGuest()
    {
        var g = new Guest();
        CalculateDiscountPercentage(g);
        Assert.Equal(0, g.discount);
        testOutputHelper.WriteLine($"for customer {nameof(g)} with type: {g.customerType} discount is {g.discount}");
    }

    [Fact]
    public void CalculateDiscountPercentage_SetDiscount_forNewCustomer()
    {
        var n = new NewCustomer();
        CalculateDiscountPercentage(n);
        Assert.Equal(5, n.discount);
        testOutputHelper.WriteLine($"for customer {nameof(n)} with type: {n.customerType} discount is {n.discount}");
    }

    [Fact]
    public void CalculateDiscountPercentage_SetDiscount_forPrime()
    {
        var p = new Prime();
        CalculateDiscountPercentage(p);
        Assert.Equal(10, p.discount);
        testOutputHelper.WriteLine($"for customer {nameof(p)} with type: {p.customerType} discount is {p.discount}");
    }

    [Fact]
    public void CalculateDiscountPercentage_SetDiscount_forElite()
    {
        var e = new Elite();
        CalculateDiscountPercentage(e);
        Assert.Equal(15, e.discount);
        testOutputHelper.WriteLine($"for customer {nameof(e)} with type: {e.customerType} discount is {e.discount}");
    }
}
