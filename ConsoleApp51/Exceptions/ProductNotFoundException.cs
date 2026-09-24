namespace ConsoleApp51.Exceptions;

internal class ProductNotFoundException : Exception
{
    public ProductNotFoundException(string message) : base(message)
    {
    }
}
