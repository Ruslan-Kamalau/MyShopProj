public interface IProduct
{
    string Name { get; }
    decimal Price { get; }
    string Category { get; }
}

public class Product : IProduct
{
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public string Category { get; private set; }

    public Product(string name, decimal price, string category)
    {
        Name = name;
        Price = price;
        Category = category;
    }
}
