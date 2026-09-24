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
    public void ApplyDiscount(ref decimal price,decimal discountPercentage)
    {
        decimal discountAmount = price * (discountPercentage / 100);
        price -= discountAmount;
    }
    private List<Product> _products = new List<Product>();
    public List<Product> SearchProducts (string searchTerm)
    {
        List<Product> result = new List<Product>();
        searchTerm = searchTerm.Trim().ToLower();
        string[]searchTerms = searchTerm.Split(' ');
        foreach (var product in _products)
        {
            string name = product.Name.Trim().ToLower();
            string description = product.Description.Trim().ToLower();
            string brand = (product is ElectronicProduct electronicProduct) ? electronicProduct.Brand.Trim().ToLower() : string.Empty;
            bool found = false;
        }
        return result;
    }
}
