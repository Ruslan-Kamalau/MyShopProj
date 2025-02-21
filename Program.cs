using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
    }
}
