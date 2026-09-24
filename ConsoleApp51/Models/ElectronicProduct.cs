using ConsoleApp51.Enum;

namespace ConsoleApp51.Models;

internal class ElectronicProduct : Product
{
    public ElectronicProduct(int id, string name, string description, decimal price, int stock, Category category, string brand, int warrantyMonths) : base(id, name, description, price, stock, category)
    {
        Brand = brand;
        WarrantyMonths = warrantyMonths;
    }

    public string Brand { get; set; } = null!;
    public int WarrantyMonths { get; set; }
    public override void GetProductInfo()
    {
        base.GetProductInfo();
        Console.WriteLine($"Brand: {Brand}");
        Console.WriteLine($"Warranty Months: {WarrantyMonths}");
    }
}
