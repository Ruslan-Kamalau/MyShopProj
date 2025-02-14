using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class Store
{
    private List<Product> products = new List<Product>();
    public event Action<Product>? ProductAdded;

    public async Task AddProductAsync(Product product)
    {
        await Task.Delay(500);
        products.Add(product);
        ProductAdded?.Invoke(product);
    }

    public IEnumerable<Product> GetProducts() => products;
}
