using ConsoleApp51.Enum;
using ConsoleApp51.Models;

namespace ConsoleApp51.Services;

internal interface IPaymentService
{
    public void Pay(int orderId, decimal amount);
    public void Refund(int orderId, decimal amount);
    public PaymentStatus GetPaymentStatus(int orderId);

}
