using ConsoleApp51.Enum;

namespace ConsoleApp51.Models;

internal class Order
{
    public int Id { get; set; }
    public Customer Customer { get; set; } = null!;
    public List<Product> Products { get; set; } = new List<Product>();
    public decimal TotalPrice { get; set; }
    public Status Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsDeleted { get; set; }
    public Order(int id, Customer customer, List<Product> products, decimal totalPrice, Status status, DateTime createdAt, bool isDeleted)
    {
        Id = id;
        Customer = customer;
        Products = products;
        TotalPrice = totalPrice;
        Status = status;
        CreatedAt = createdAt;
        IsDeleted = isDeleted;
    }

}
