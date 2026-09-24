namespace ConsoleApp51.Exceptions;

internal class OrderNotFoundException : Exception
{
    public OrderNotFoundException(string message) : base(message)
    {
    }
}
