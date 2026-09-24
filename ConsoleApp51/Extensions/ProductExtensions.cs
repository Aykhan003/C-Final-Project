using ConsoleApp51.Models;

namespace ConsoleApp51.Extensions;

internal static class ProductExtensions
{
    public static bool IsInStock(this Product product)
    {
        return product.Stock > 0;
    }
    public static void GetFinalPrice(this Product product, decimal discountPercentage)
    {
        decimal discountAmount = product.Price * (discountPercentage / 100);
        decimal finalPrice = product.Price - discountAmount;
        Console.WriteLine($"Final Price after {discountPercentage}% discount: {finalPrice}");
    }
    public static bool IsExpensive(this Product product, decimal threshold)
    {
        return product.Price > threshold;
    }
}
