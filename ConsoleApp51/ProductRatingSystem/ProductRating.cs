using ConsoleApp51.Models;

namespace ConsoleApp51.ProductRatingSystem;

internal class ProductRating
{
    public int Id { get; set; }
    public Product Product { get; set; }
    public Customer Customer { get; set; }
    public int Score
    {
        get { return field; }
        set
        {
            if (value < 1 || value > 5)
            {
                throw new ArgumentOutOfRangeException(nameof(Score), "Score must be between 1 and 5.");
            }
            field = value;
        }
    }
    public string Comment { get; set; }
    public DateTime CreatedAt { get; set; }
}
