namespace ConsoleApp51.Exceptions;

internal class OutOfStockException : Exception
{
    public OutOfStockException(string message) : base(message)
    {
}
