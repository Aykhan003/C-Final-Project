namespace ConsoleApp51.Models;

internal class Customer
{
    private Guid guid;
    private string name;
    private string phone;

    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public Customer(int id, string firstName, string lastName, string email, string phoneNumber)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
    }

    public Customer(Guid guid, string name, string email, string phone)
    {
        this.guid = guid;
        this.name = name;
        Email = email;
        this.phone = phone;
    }

    public string FullName => $"{FirstName} {LastName}";
}
