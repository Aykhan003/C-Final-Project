using ConsoleApp51.Enum;

namespace ConsoleApp51.Models;

internal class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public Category Category { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public Product(int id, string name, string description, decimal price, int stock, Category category)
    {
        Id = id;
        Name = name;
        Description = description;
        Price = price;
        Stock = stock;
        Category = category;
    }
    public virtual void GetProductInfo()
    {
        Console.WriteLine($"Id: {Id}");
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Description: {Description}");
        Console.WriteLine($"Price: {Price}");
        Console.WriteLine($"Stock: {Stock}");
        Console.WriteLine($"Category: {Category}");
        Console.WriteLine($"IsDeleted: {IsDeleted}");
        Console.WriteLine($"CreatedAt: {CreatedAt}");
    }
    public void CalculateDiscount(decimal discountPercentage)
    {
        decimal discountAmount = Price * (discountPercentage / 100);
        decimal discountedPrice = Price - discountAmount;
        Console.WriteLine($"Discounted Price: {discountedPrice}");
    }
}
