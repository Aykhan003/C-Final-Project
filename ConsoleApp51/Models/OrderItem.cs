namespace ConsoleApp51.Models;

internal class OrderItem
{
    public Product Product { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
    public OrderItem(Product product, int quantity, decimal unitPrice, decimal totalPrice)
    {
        Product = product;
        Quantity = quantity;
        UnitPrice = unitPrice;
        TotalPrice = totalPrice;
    }
    public bool TryRemoveFromStock(Product product, int quantity, out decimal price)
    {
        if (product.Stock >= quantity)
        {
            product.Stock -= quantity;
            price = product.Price * quantity;
            return true;
        }
        price = 0;
        return false;
    }
}
