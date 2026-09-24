using ConsoleApp51.Services;

namespace ConsoleApp51.Models;

internal class OrderService : IOrderService
{
    private List<Order> _orders = new List<Order>();
    public Order CreateOrder(Customer customer)
    {
        var order = new Order(_orders.Count + 1, customer, new List<Product>(), 0, Enum.Status.Pending, DateTime.Now, false);
        _orders.Add(order);
        return order;
    }
    public void AddProductToOrder(int orderId, Product product)
    {
        var order = _orders.FirstOrDefault(o => o.Id == orderId);
        if (order != null)
        {
            order.Products.Add(product);
            order.TotalPrice += product.Price;
        }
    }
    public void RemoveProductFromOrder(int orderId, Product product)
    {
        var order = _orders.FirstOrDefault(o => o.Id == orderId);
        if (order != null)
        {
            order.Products.Remove(product);
            order.TotalPrice -= product.Price;
        }
    }
    public void ConfirmOrder(int orderId)
    {
        var order = _orders.FirstOrDefault(o => o.Id == orderId);
        if (order != null)
        {
            order.Status = Enum.Status.Confirmed;
        }
    }
    public void CancelOrder(int orderId)
    {
        var order = _orders.FirstOrDefault(o => o.Id == orderId);
        if (order != null)
        {
            order.Status = Enum.Status.Cancelled;
        }
    }
    public Order GetOrder(int id)
    {
        return _orders.FirstOrDefault(o => o.Id == id) ?? throw new Exception("Order not found");
    }
    public List<Order> GetCustomerOrders(int customerId)
    {
        return _orders.Where(o => o.Customer.Id == customerId).ToList();
    }
}
