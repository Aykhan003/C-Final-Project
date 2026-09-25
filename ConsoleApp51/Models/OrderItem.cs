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

    public OrderItem(Product product, int v)
    {
        Product = product;
        this.v = v;
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
    public void ApplyDiscount(ref decimal price,decimal discountPercentage)
    {
        decimal discountAmount = price * (discountPercentage / 100);
        price -= discountAmount;
    }
    private List<Product> _products = new List<Product>();
    private int v;

    public List<Product> SearchProducts (string searchTerm)
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
