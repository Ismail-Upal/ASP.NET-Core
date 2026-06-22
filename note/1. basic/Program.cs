namespace CSharpBasics;

public interface IDiscountable
{
    decimal ApplyDiscount(decimal percentage);
}

class Product
{
    private decimal _price;
    public string Name { get; set; }
    public decimal Price
    {
        get { return _price; }
        set
        {
            if(value >= 0) _price = value;
        }
    }

    public Product(string name, decimal price)
    {
        Name = name;
        Price = price;
    }

    public virtual void DisplayProductDetails()
    {
        Console.WriteLine($"Product: {Name}, Price: {Price}");
    }

    public static decimal CalculateDiscount(decimal price, decimal discountPercentage)
    {
        return price - (price * discountPercentage / 100);
    }
}


class Clothing : Product, IDiscountable
{
    public int Size { get; set; }
    public Clothing(string name, decimal price, int size) : base(name, price)
    {
        Size = size;
    }

    public string GetSizeName()
    {
        return Size switch
        {
            1 => "SM",
            2 => "MD",
            3 => "LG",
            _ => "Unknown size"
        };
    }

    public override void DisplayProductDetails()
    {
        base.DisplayProductDetails();
        Console.WriteLine($"Size: {GetSizeName()}");
    }

    public decimal ApplyDiscount(decimal percentage)
    {
        return CalculateDiscount(Price, percentage);
    }
}


class Program
{
    static void Main()
    {
        List<Clothing> catalog = new List<Clothing>();
        catalog.Add(new Clothing("T-shirt", 324.4m, 3));
        catalog.Add(new Clothing("pants", 324.4m, 3));
        catalog.Add(new Clothing("Cap", 324.4m, 3));

        foreach(Clothing item in catalog)
        {
            item.DisplayProductDetails();
        }

        decimal decountedPrice = catalog[0].ApplyDiscount(10);
        Console.WriteLine($"T-shirt price after discount: {decountedPrice:c}");
        Console.WriteLine(Product.CalculateDiscount(34.34m, 0.3m));

    }
}

/*
nuget: 
newtonsoft.json
dapper
serilog


*/