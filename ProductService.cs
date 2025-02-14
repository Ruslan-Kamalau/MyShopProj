using System;
using System.Collections.Generic;
using System.Linq;

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
