using ConsoleApp51.Services;

namespace ConsoleApp51.Models;

internal class ProductService : IProductService
{
    private List<Product> _products = new List<Product>();
    public void AddProduct(Product product)
    {
        _products.Add(product);
    }
    public void RemoveProduct(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product != null)
        {
            product.IsDeleted = true;
        }
    }
    public void RestoreProduct(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product != null)
        {
            product.IsDeleted = false;
        }
    }
    public Product GetProduct(int id)
    {
        return _products.FirstOrDefault(p => p.Id == id) ?? throw new Exception("Product not found");
    }
    public List<Product> GetAllProducts()
    {
        return _products.Where(p => !p.IsDeleted).ToList();
    }
    public List<Product> SearchProducts(string searchTerm)
    {
        searchTerm = searchTerm.Trim().ToLower().Replace("-", " ");
        string[] words = searchTerm.Split(' ');
        return _products.Where(p =>
            words.Any(word =>
                p.Name.ToLower().Contains(word) ||
                p.Description.ToLower().Contains(word) ||
                p.Name.ToLower().StartsWith(word) ||
                p.Name.ToLower().EndsWith(word) ||
                p.Description.ToLower().StartsWith(word) ||
                p.Description.ToLower().EndsWith(word)
            )
        ).ToList();
    }
}
