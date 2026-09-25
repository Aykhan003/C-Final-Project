using ConsoleApp51.Enum;
using ConsoleApp51.Services;

namespace ConsoleApp51.PaymentMethods;

internal class CashPayment : IPaymentService
{
    private PaymentStatus _paymentStatus = PaymentStatus.Pending;
    public void Pay(int orderId, decimal amount)
    {
        Console.WriteLine($"Processing cash payment for Order ID: {orderId}, Amount: {amount}");
        _paymentStatus = PaymentStatus.Paid;
    }
    public void Refund(int orderId, decimal amount)
    {
        if (_paymentStatus == PaymentStatus.Paid)
        {
        Console.WriteLine($"Processing cash refund for Order ID: {orderId}, Amount: {amount}");
            _paymentStatus = PaymentStatus.Refunded;
        }
    }
    public PaymentStatus GetPaymentStatus(int orderId)
    {
        return _paymentStatus;
    }
}
