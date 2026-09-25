using ConsoleApp51.Services;

namespace ConsoleApp51.Models;

internal class ProductService : IProductService
{
    private List<Product> _products = new List<Product>();
    private List<Order> _orders = new List<Order>();
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
    public Product GetMostExpensiveProduct()
    {
        return _products.Where(p => !p.IsDeleted).OrderByDescending(p => p.Price).FirstOrDefault() ?? throw new Exception("No products available");
    }
    public Product GetCheapestProduct()
    {
        return _products.Where(p => !p.IsDeleted).OrderBy(p => p.Price).FirstOrDefault() ?? throw new Exception("No products available");
    }
    public List<Product> GetAvailableProducts()
    {
        return _products.Where(p => !p.IsDeleted && p.Stock > 0).ToList();
    }
    public List<Product> GetOutOfStockProducts()
    {
        return _products.Where(p => !p.IsDeleted && p.Stock == 0).ToList();
    }
    public List<Product> GetProductsByPrice(decimal minPrice, decimal maxPrice)
    {
        return _products.Where(p => !p.IsDeleted && p.Price >= minPrice && p.Price <= maxPrice).ToList();
    }
    public Product GetBestSellingProduct()
    {
        return _orders.SelectMany(o => o.Products).GroupBy(p => p.Id).OrderByDescending(g => g.Count()).Select(g => g.First()).FirstOrDefault() ?? throw new Exception("No products available");
    }
    public List<Product> GetCustomerOrders(int customerId)
    {
        return _orders.Where(o => o.Customer.Id == customerId).SelectMany(o => o.Products).ToList();
    }
    public List<Product> GetDeletedProducts()
    {
        return _products.Where(p => p.IsDeleted).ToList();
    }
}
