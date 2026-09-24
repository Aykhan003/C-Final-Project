using ConsoleApp51.Enum;

namespace ConsoleApp51.Models;

internal class ClothingProduct : Product
{
    public ClothingProduct(int id, string name, string description, decimal price, int stock, Category category, string size, string material, string gender) : base(id, name, description, price, stock, category)
    {
        Size = size;
        Material = material;
        Gender = gender;
    }
    public string Size { get; set; } = null!;
    public string Material { get; set; } = null!;
    public string Gender { get; set; } = null!;
    public override void GetProductInfo()
    {
        base.GetProductInfo();
        Console.WriteLine($"Size: {Size}");
        Console.WriteLine($"Material: {Material}");
        Console.WriteLine($"Gender: {Gender}");
    }
}
