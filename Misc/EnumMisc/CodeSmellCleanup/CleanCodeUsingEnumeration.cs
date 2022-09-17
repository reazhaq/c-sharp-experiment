namespace Misc.EnumMisc.CodeSmellCleanup;

/// <summary>
/// from https://lostechies.com/jimmybogard/2008/08/12/enumeration-classes/
/// </summary>
public class ImprovedCustomerTypes : Enumeration
{
    public int Discount { get; }

    private ImprovedCustomerTypes(int value, string displayName, int discount) : base(value, displayName)
    {
        Discount = discount;
    }

    public static readonly ImprovedCustomerTypes Guest = new(0, "Guest", 0);
    public static readonly ImprovedCustomerTypes NewCustomer = new(1, "NewCustomer", 5);
    public static readonly ImprovedCustomerTypes Prime = new(2, "Prime", 10);
    public static readonly ImprovedCustomerTypes Elite = new(3, "Elite", 15);
}


public class Customer
{
    public ImprovedCustomerTypes customerType { get; }
    public int discount { get; set; }
    public Customer(ImprovedCustomerTypes customerType) => this.customerType = customerType;
}

public class Guest : Customer
{
    public Guest() : base(ImprovedCustomerTypes.Guest) { }
}

public class NewCustomer : Customer
{
    public NewCustomer() : base(ImprovedCustomerTypes.NewCustomer) { }
}

public class Prime : Customer
{
    public Prime() : base(ImprovedCustomerTypes.Prime) { }
}

public class Elite : Customer
{
    public Elite() : base(ImprovedCustomerTypes.Elite) { }
}


public class CleanCodeUsingEnumeration
{
    private readonly ITestOutputHelper testOutputHelper;

    public CleanCodeUsingEnumeration(ITestOutputHelper testOutputHelper)
    {
        this.testOutputHelper = testOutputHelper;
    }

    private void CalculateDiscountPercentage(Customer customer)
    {
        if (customer == null)
            throw new ArgumentNullException(nameof(customer));

        customer.discount = customer.customerType.Discount;
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
