using ConsoleApp51.Enum;
using ConsoleApp51.Models;

namespace ConsoleApp51.SalesStaticsSystem;

internal class SalesStatisticsService
{
    private List<Order> _orders;
    public SalesStatisticsService(List<Order> orders)
    {
        _orders = orders;
    }
    public decimal GetTotalOrders()
    {
        return _orders.Count;
    }
    public decimal GetAverageOrderValue()
    {
        if (_orders.Count == 0)
        {
            return 0;
        }
        return _orders.Average(o => o.TotalPrice);
    }
    public Product GetBestSellingProduct()
    {
        var productSales = new Dictionary<Product, int>();
        foreach (var order in _orders)
        {
            foreach (var product in order.Products)
            {
                if (!productSales.ContainsKey(product))
                {
                    productSales[product] = 0;
                }
                productSales[product]++;
            }
        }
        return productSales.OrderByDescending(p => p.Value).FirstOrDefault().Key;
    }
    public Customer GetBestCustomer()
    {
        var customerSales = new Dictionary<Customer, decimal>();
        foreach (var order in _orders)
        {
            if (!customerSales.ContainsKey(order.Customer))
            {
                customerSales[order.Customer] = 0;
            }
            customerSales[order.Customer] += order.TotalPrice;
        }
        return customerSales.OrderByDescending(c => c.Value).FirstOrDefault().Key;
    }
    public Dictionary<Category, decimal> GetSalesByCategory()
    {
        var categorySales = new Dictionary<Category, decimal>();
        foreach (var order in _orders)
        {
            foreach (var product in order.Products)
            {
                if (!categorySales.ContainsKey(product.Category))
                {
                    categorySales[product.Category] = 0;
                }
                categorySales[product.Category] += product.Price;
            }
        }
        return categorySales;
    }
    public Dictionary<DateTime,decimal> GetSalesByDate()
    {
        var salesByDate = new Dictionary<DateTime, decimal>();
        foreach (var order in _orders)
        {
            var date = order.CreatedAt.Date;
            if (!salesByDate.ContainsKey(date))
            {
                salesByDate[date] = 0;
            }
            salesByDate[date] += order.TotalPrice;
        }
        return salesByDate;
    }

}
