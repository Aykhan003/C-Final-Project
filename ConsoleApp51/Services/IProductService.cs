using ConsoleApp51.Models;

namespace ConsoleApp51.Services;

internal interface IProductService
{
    public void AddProduct(Product product);
    public void RemoveProduct(int id);
    public void RestoreProduct(int id);
    public Product GetProduct(int id);
    public List<Product> GetAllProducts();
    public List<Product> SearchProducts(string searchTerm);
    List<Product> GetDeletedProducts();
}
