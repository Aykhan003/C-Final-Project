namespace ConsoleApp51.Models;

internal class ProductCollection
{
    private List<Product> products = new List<Product>();
    public Product this[int index]
    {
        get { return products[index]; }
    }
    public Product this[string name]
    {
        get { return products.FirstOrDefault(p => p.Name == name) ?? throw new ArgumentException($"Product with name '{name}' not found."); }
    }
    public void Add(Product product)
    {
        products.Add(product);
    }
}
