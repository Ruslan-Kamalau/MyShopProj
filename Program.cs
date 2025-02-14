using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

public delegate void ProductAddedHandler(Product product);

public class Store
{
    private List<Product> products = new List<Product>();
    public event ProductAddedHandler? ProductAdded;

    public async Task AddProductAsync(Product product)
    {
        await Task.Delay(500);
        products.Add(product);
        ProductAdded?.Invoke(product);
    }

    public IEnumerable<Product> GetProducts() => products;
}

public class ProductService
{
    private readonly IEnumerable<Product> _products;
    
    public ProductService(IEnumerable<Product> products)
    {
        _products = products;
    }

    public IEnumerable<Product> GetFilteredProducts(string category) =>
        _products.Where(p => p.Category == category);

    public IEnumerable<string> GetProductNamesSortedByPrice() =>
        _products.OrderBy(p => p.Price).Select(p => p.Name);
    
    public decimal GetTotalPrice() => _products.Sum(p => p.Price);
    
    public decimal GetAveragePrice() => _products.Average(p => p.Price);
    
    public int GetProductCount() => _products.Count();
    
    public IEnumerable<IGrouping<string, Product>> GetGroupedByCategory() =>
        _products.GroupBy(p => p.Category);
}

public class OrderProcessor
{
    public async Task ProcessOrdersAsync(List<string> orders)
    {
        await Parallel.ForEachAsync(orders, async (order, _) =>
        {
            Console.WriteLine($"Обработка заказа: {order}");
            await Task.Delay(1000);
            Console.WriteLine($"Заказ {order} обработан");
        });
    }
}

public class Program
{
    public static async Task Main()
    {
        Store store = new Store();
        store.ProductAdded += product => Console.WriteLine($"Добавлен товар: {product.Name}, {product.Price}");

        await store.AddProductAsync(new Product("Laptop", 1000m, "Electronics"));
        await store.AddProductAsync(new Product("Shirt", 30m, "Clothing"));
        await store.AddProductAsync(new Product("Phone", 500m, "Electronics"));
        
        ProductService productService = new ProductService(store.GetProducts());

        Console.WriteLine("Фильтрация (только Electronics):");
        foreach (var product in productService.GetFilteredProducts("Electronics"))
            Console.WriteLine(product.Name);
        
        Console.WriteLine("Сортировка по цене:");
        foreach (var name in productService.GetProductNamesSortedByPrice())
            Console.WriteLine(name);
        
        Console.WriteLine($"Общая стоимость товаров: {productService.GetTotalPrice()}");
        Console.WriteLine($"Средняя цена: {productService.GetAveragePrice()}");
        Console.WriteLine($"Количество товаров: {productService.GetProductCount()}");
        
        Console.WriteLine("Группировка по категориям:");
        foreach (var group in productService.GetGroupedByCategory())
        {
            Console.WriteLine($"Категория: {group.Key}");
            foreach (var product in group)
                Console.WriteLine($" - {product.Name}");
        }

        OrderProcessor orderProcessor = new OrderProcessor();
        List<string> orders = new List<string> { "Order1", "Order2", "Order3", "Order4" };
        await orderProcessor.ProcessOrdersAsync(orders);
    }
}
