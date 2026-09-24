using ConsoleApp51.Models;

namespace ConsoleApp51.Services;

internal interface IOrderService
{
    public Order CreateOrder(Customer customer);
    public void AddProductToOrder(int orderId, Product product);
    public void RemoveProductFromOrder(int orderId, Product product);
    public void ConfirmOrder(int orderId);
    public void CancelOrder(int orderId);
    public Order GetOrder(int id);
    public List<Order> GetCustomerOrders(int customerId);

}
