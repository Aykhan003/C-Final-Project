using ConsoleApp51.Models;

namespace ConsoleApp51.Pagination;

public class PaginationService
{
    private List<Product> _products;
    public PaginationService(List<Product> products)
    {
        _products = products;
    }
    public List<Product> GetProducts(int pageNumber, int pageSize)
    {
        return _products.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
    }
}
