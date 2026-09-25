using ConsoleApp51.Models;

namespace ConsoleApp51.ProductRatingSystem;

internal class RatingService
{
    private List<ProductRating> _ratings = new List<ProductRating>();
    private List<Order> _orders;
    public RatingService(List<Order> orders)
    {
        _orders = orders;
    }
    public double GetAverageRating(Product product)
    {
        var productRatings = _ratings.Where(r => r.Product.Id == product.Id);
        if (!productRatings.Any())
        {
            return 0;
        }
        return productRatings.Average(r => r.Score);
    }
    public List<Product> GetHighestRatedProducts()
    {
        return _ratings
            .GroupBy(r => r.Product)
            .Select(g => new { Product = g.Key, AverageScore = g.Average(r => r.Score) })
            .OrderByDescending(p => p.AverageScore)
            .Select(p => p.Product)
            .ToList();
    }
    public List<Product> GetLowestRatedProducts()
    {
        return _ratings
            .GroupBy(r => r.Product)
            .Select(g => new { Product = g.Key, AverageScore = g.Average(r => r.Score) })
            .OrderBy(p => p.AverageScore)
            .Select(p => p.Product)
            .ToList();
    }
}
