namespace ConsoleApp51.Exceptions;

internal class CustomerNotFoundException : Exception
{
    public CustomerNotFoundException(string message) : base(message)
    {
    }
}
