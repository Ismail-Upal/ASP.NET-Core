using Newtonsoft.Json;

public class Product
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public List<string> Tags { get; set; }
}

public class Program
{
    static void Main()
    {
        string json =  "{\"Name\": \"ismail\", \"Price\": 99.33, \"Tags\": [\"Electronics\", \"Computer\"]}";

        Product product = JsonConvert.DeserializeObject<Product>(json);
        
        Console.WriteLine($"Product: {product.Name}");



        Product newProduct = new Product
        {
            Name = "phone",
            Price = 243.43m,
            Tags = new List<string> {"electronics", "mobile"}
        };

        string newJson = JsonConvert.SerializeObject(newProduct, Formatting.Indented);
        Console.WriteLine($"Serialized JSON: \n{newJson}");
    }
}