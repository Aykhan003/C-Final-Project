using ConsoleApp51.Models;

namespace ConsoleApp51.Collections;

internal class ProductCollection
{
    private List<Product> _products = new();
    public Product this[int index]
    {
        get { return _products[index]; }
    }
    public Product this[string name]
    {
        get { return _products.FirstOrDefault(p => p.Name == name) ?? throw new KeyNotFoundException($"Product with name '{name}' not found."); }
    }
    public void Add(Product product)
    {
        _products.Add(product);
    }
}
