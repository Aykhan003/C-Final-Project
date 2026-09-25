using ConsoleApp51.Models;
using ConsoleApp51.Services;
using ConsoleApp51.Collections;

namespace ConsoleApp51;

class Program
{
    private static OrderService _service = new OrderService();

    static void Main()
    {
        while (true)
        {
            DisplayMainMenu();
            string choice = Console.ReadLine()?.ToUpper() ?? "";

            try
            {
                switch (choice)
                {
                    case "1":
                        AddCustomer();
                        break;
                    case "2":
                        AddProduct();
                        break;
                    case "3":
                        ShowProducts();
                        break;
                    case "4":
                        SearchProduct();
                        break;
                    case "5":
                        FilterProducts();
                        break;
                    case "6":
                        CreateOrder();
                        break;
                    case "7":
                        AddProductToOrder();
                        break;
                    case "8":
                        RemoveProductFromOrder();
                        break;
                    case "9":
                        ShowOrder();
                        break;
                    case "10":
                        ConfirmOrder();
                        break;
                    case "11":
                        CancelOrder();
                        break;
                    case "12":
                        ShowCustomerOrders();
                        break;
                    case "13":
                        DeleteProduct();
                        break;
                    case "14":
                        RestoreProduct();
                        break;
                    case "15":
                        ShowDeletedProducts();
                        break;
                    case "16":
                        ProductStatistics();
                        break;
                    case "17":
                        ObjectInspector();
                        break;
                    case "18":
                        GarbageCollectionTest();
                        break;
                    case "0":
                        Console.WriteLine("Proqramdan çıxırız...");
                        return;
                    default:
                        Console.WriteLine("❌ Yanlış seçim!");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Xəta: {ex.Message}");
            }

            Console.WriteLine("\nDevam etmək üçün daxil edin...");
            Console.ReadLine();
            Console.Clear();
        }
    }

    static void DisplayMainMenu()
    {
        Console.Clear();
        Console.WriteLine("========================================");
        Console.WriteLine("           🛒 SHOPHUB");
        Console.WriteLine("========================================\n");
        Console.WriteLine("1. Add Customer");
        Console.WriteLine("2. Add Product");
        Console.WriteLine("3. Show Products");
        Console.WriteLine("4. Search Product");
        Console.WriteLine("5. Filter Products");
        Console.WriteLine("6. Create Order");
        Console.WriteLine("7. Add Product To Order");
        Console.WriteLine("8. Remove Product From Order");
        Console.WriteLine("9. Show Order");
        Console.WriteLine("10. Confirm Order");
        Console.WriteLine("11. Cancel Order");
        Console.WriteLine("12. Show Customer Orders");
        Console.WriteLine("13. Delete Product");
        Console.WriteLine("14. Restore Product");
        Console.WriteLine("15. Show Deleted Products");
        Console.WriteLine("16. Product Statistics");
        Console.WriteLine("17. Object Inspector");
        Console.WriteLine("18. Garbage Collection Test");
        Console.WriteLine("\n0. Exit");
        Console.WriteLine("========================================\n");
        Console.Write("Seçim edin: ");
    }

    static void AddCustomer()
    {
        Console.Write("Müştəri adı: ");
        string name = Console.ReadLine() ?? "";

        Console.Write("Email: ");
        string email = Console.ReadLine() ?? "";

        Console.Write("Telefon: ");
        string phone = Console.ReadLine() ?? "";

        var customer = new Customer(Guid.NewGuid(), name, email, phone);
        _service.AddCustomer(customer);
        Console.WriteLine("✅ Müştəri əlavə olundu!");
    }

    static void AddProduct()
    {
        Console.Write("Məhsul adı: ");
        string name = Console.ReadLine() ?? "";

        Console.Write("Qiymət: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal price))
        {
            throw new ArgumentException("Yanlış qiymət dəyəri!");
        }

        Console.Write("Kateqoriya: ");
        string category = Console.ReadLine() ?? "";

        Console.Write("Stok sayı: ");
        if (!int.TryParse(Console.ReadLine(), out int stock))
        {
            throw new ArgumentException("Yanlış stok dəyəri!");
        }

        var product = new Product(Guid.NewGuid(), name, price, category, stock);
        _service.AddProduct(product);
        Console.WriteLine("✅ Məhsul əlavə olundu!");
    }

    static void ShowProducts()
    {
        var products = _service.GetAllProducts();
        if (products.Count == 0)
        {
            Console.WriteLine("❌ Məhsul yoxdur!");
            return;
        }

        Console.WriteLine("\n📦 Məhsullar:");
        Console.WriteLine("────────────────────────────────────────");
        foreach (var p in products)
        {
            Console.WriteLine($"ID: {p.Id}");
            Console.WriteLine($"Adı: {p.Name}");
            Console.WriteLine($"Qiymət: {p.Price:C}");
            Console.WriteLine($"Kateqoriya: {p.Category}");
            Console.WriteLine($"Stok: {p.Stock}");
            Console.WriteLine($"Rating Ortalaması: {p.GetAverageRating():F2}⭐");
            Console.WriteLine("────────────────────────────────────────");
        }
    }

    static void SearchProduct()
    {
        Console.Write("Axtarış sorğusu: ");
        string query = Console.ReadLine() ?? "";

        var results = _service.SearchProducts(query);
        if (results.Count == 0)
        {
            Console.WriteLine("❌ Məhsul tapılmadı!");
            return;
        }

        Console.WriteLine("\n🔍 Axtarış nəticələri:");
        foreach (var p in results)
        {
            Console.WriteLine($"- {p.Name} ({p.Price:C})");
        }
    }

    static void FilterProducts()
    {
        Console.Write("Kateqoriya: ");
        string category = Console.ReadLine() ?? "";

        Console.Write("Min qiymət: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal minPrice))
            minPrice = 0;

        Console.Write("Max qiymət: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal maxPrice))
            maxPrice = decimal.MaxValue;

        var filtered = _service.FilterProducts(category, minPrice, maxPrice);
        if (filtered.Count == 0)
        {
            Console.WriteLine("❌ Filtrlənmiş məhsul yoxdur!");
            return;
        }

        Console.WriteLine("\n🎯 Filtrlənmiş məhsullar:");
        foreach (var p in filtered)
        {
            Console.WriteLine($"- {p.Name} ({p.Price:C}) - Kateqoriya: {p.Category}");
        }
    }

    static void CreateOrder()
    {
        Console.Write("Müştəri ID-ni daxil edin: ");
        if (!Guid.TryParse(Console.ReadLine(), out var customerId))
        {
            throw new ArgumentException("Yanlış müştəri ID!");
        }

        var customer = _service.GetCustomerById(customerId);
        if (customer == null)
        {
            throw new InvalidOperationException("Müştəri tapılmadı!");
        }

        var order = new Order(Guid.NewGuid(), customerId);
        _service.AddOrder(order);
        Console.WriteLine($"✅ Sifariş yaradıldı! Sifariş ID: {order.Id}");
    }

    static void AddProductToOrder()
    {
        Console.Write("Sifariş ID-ni daxil edin: ");
        if (!Guid.TryParse(Console.ReadLine(), out var orderId))
        {
            throw new ArgumentException("Yanlış sifariş ID!");
        }

        Console.Write("Məhsul ID-ni daxil edin: ");
        if (!Guid.TryParse(Console.ReadLine(), out var productId))
        {
            throw new ArgumentException("Yanlış məhsul ID!");
        }

        Console.Write("Miqdar: ");
        if (!int.TryParse(Console.ReadLine(), out int quantity))
        {
            throw new ArgumentException("Yanlış miqdar!");
        }

        _service.AddProductToOrder(orderId, productId, quantity);
        Console.WriteLine("✅ Məhsul sifariş-ə əlavə olundu!");
    }

    static void RemoveProductFromOrder()
    {
        Console.Write("Sifariş ID-ni daxil edin: ");
        if (!Guid.TryParse(Console.ReadLine(), out var orderId))
        {
            throw new ArgumentException("Yanlış sifariş ID!");
        }

        Console.Write("Məhsul ID-ni daxil edin: ");
        if (!Guid.TryParse(Console.ReadLine(), out var productId))
        {
            throw new ArgumentException("Yanlış məhsul ID!");
        }

        _service.RemoveProductFromOrder(orderId, productId);
        Console.WriteLine("✅ Məhsul sifarişdən çıxarıldı!");
    }

    static void ShowOrder()
    {
        Console.Write("Sifariş ID-ni daxil edin: ");
        if (!Guid.TryParse(Console.ReadLine(), out var orderId))
        {
            throw new ArgumentException("Yanlış sifariş ID!");
        }

        var order = _service.GetOrderById(orderId);
        if (order == null)
        {
            throw new InvalidOperationException("Sifariş tapılmadı!");
        }

        Console.WriteLine($"\n📋 Sifariş #{order.Id}");
        Console.WriteLine($"Müştəri ID: {order.CustomerId}");
        Console.WriteLine($"Status: {order.Status}");
        Console.WriteLine($"Tarix: {order.CreatedAt}");
        Console.WriteLine("────────────────────────────────────────");

        decimal totalPrice = 0;
        foreach (var item in order.Items)
        {
            var product = _service.GetProductById(item.ProductId);
            if (product != null)
            {
                decimal itemTotal = product.Price * item.Quantity;
                totalPrice += itemTotal;
                Console.WriteLine($"- {product.Name} x{item.Quantity} = {itemTotal:C}");
            }
        }

        Console.WriteLine("────────────────────────────────────────");
        Console.WriteLine($"Cəmi: {totalPrice:C}");
    }

    static void ConfirmOrder()
    {
        Console.Write("Sifariş ID-ni daxil edin: ");
        if (!Guid.TryParse(Console.ReadLine(), out var orderId))
        {
            throw new ArgumentException("Yanlış sifariş ID!");
        }

        var order = _service.GetOrderById(orderId);
        if (order == null)
        {
            throw new InvalidOperationException("Sifariş tapılmadı!");
        }

        Console.Write("Ödəniş methodu seçin (1-Cash, 2-Card, 3-Online): ");
        string paymentMethod = Console.ReadLine() ?? "1";

        Console.Write("Promo kod var mı? (Yox olarsa boş buraxın): ");
        string promoCode = Console.ReadLine() ?? "";

        _service.ConfirmOrder(orderId, paymentMethod, promoCode);
        Console.WriteLine("✅ Sifariş təsdiqləndi!");
    }

    static void CancelOrder()
    {
        Console.Write("Sifariş ID-ni daxil edin: ");
        if (!Guid.TryParse(Console.ReadLine(), out var orderId))
        {
            throw new ArgumentException("Yanlış sifariş ID!");
        }

        _service.CancelOrder(orderId);
        Console.WriteLine("✅ Sifariş ləğv olundu!");
    }

    static void ShowCustomerOrders()
    {
        Console.Write("Müştəri ID-ni daxil edin: ");
        if (!Guid.TryParse(Console.ReadLine(), out var customerId))
        {
            throw new ArgumentException("Yanlış müştəri ID!");
        }

        var orders = _service.GetCustomerOrders(customerId);
        if (orders.Count == 0)
        {
            Console.WriteLine("❌ Sifariş yoxdur!");
            return;
        }

        Console.WriteLine("\n📦 Müştəri Sifarişləri:");
        foreach (var order in orders)
        {
            Console.WriteLine($"- Sifariş ID: {order.Id}, Status: {order.Status}, Tarix: {order.CreatedAt}");
        }
    }

    static void DeleteProduct()
    {
        Console.Write("Məhsul ID-ni daxil edin: ");
        if (!Guid.TryParse(Console.ReadLine(), out var productId))
        {
            throw new ArgumentException("Yanlış məhsul ID!");
        }

        _service.SoftDeleteProduct(productId);
        Console.WriteLine("✅ Məhsul silindi! (Soft Delete)");
    }

    static void RestoreProduct()
    {
        Console.Write("Məhsul ID-ni daxil edin: ");
        if (!Guid.TryParse(Console.ReadLine(), out var productId))
        {
            throw new ArgumentException("Yanlış məhsul ID!");
        }

        _service.RestoreProduct(productId);
        Console.WriteLine("✅ Məhsul bərpa olundu!");
    }

    static void ShowDeletedProducts()
    {
        var deleted = _service.GetDeletedProducts();
        if (deleted.Count == 0)
        {
            Console.WriteLine("❌ Silinmiş məhsul yoxdur!");
            return;
        }

        Console.WriteLine("\n🗑️ Silinmiş Məhsullar:");
        foreach (var p in deleted)
        {
            Console.WriteLine($"- {p.Name} ({p.Price:C})");
        }
    }

    static void ProductStatistics()
    {
        Console.WriteLine("\n📊 Məhsul Statistikası:");
        Console.WriteLine($"Ümumi məhsul: {_service.GetTotalProducts()}");
        Console.WriteLine($"Silinmiş məhsul: {_service.GetDeletedProductsCount()}");
        Console.WriteLine($"Ümumi müştəri: {_service.GetTotalCustomers()}");
        Console.WriteLine($"Ümumi sifariş: {_service.GetTotalOrders()}");

        var highestRated = _service.GetHighestRatedProducts(5);
        if (highestRated.Count > 0)
        {
            Console.WriteLine("\n⭐ Ən yüksək rəyinə sahib məhsullar:");
            foreach (var p in highestRated)
            {
                Console.WriteLine($"- {p.Name}: {p.GetAverageRating():F2}⭐");
            }
        }
    }

    static void ObjectInspector()
    {
        Console.WriteLine("\n🔍 Object Inspector");
        Console.WriteLine("1. Müştəriləri göstər");
        Console.WriteLine("2. Məhsulları göstər");
        Console.WriteLine("3. Sifarişləri göstər");
        Console.Write("Seçim edin: ");

        string choice = Console.ReadLine() ?? "";

        switch (choice)
        {
            case "1":
                InspectCustomers();
                break;
            case "2":
                InspectProducts();
                break;
            case "3":
                InspectOrders();
                break;
        }
    }

    static void InspectCustomers()
    {
        var customers = _service.GetAllCustomers();
        Console.WriteLine($"\n👥 Ümumi Müştəri: {customers.Count}");
        foreach (var c in customers)
        {
            Console.WriteLine($"- {c.Name} ({c.Email})");
        }
    }

    static void InspectProducts()
    {
        var products = _service.GetAllProducts();
        Console.WriteLine($"\n📦 Ümumi Məhsul: {products.Count}");
        foreach (var p in products)
        {
            Console.WriteLine($"- {p.Name}: {p.Price:C} (Stok: {p.Stock})");
        }
    }

    static void InspectOrders()
    {
        var orders = _service.GetAllOrders();
        Console.WriteLine($"\n📋 Ümumi Sifariş: {orders.Count}");
        foreach (var o in orders)
        {
            Console.WriteLine($"- Sifariş #{o.Id}: {o.Status}");
        }
    }

    static void GarbageCollectionTest()
    {
        Console.WriteLine("\n🧹 Garbage Collection Test");
        long beforeMemory = GC.GetTotalMemory(false);
        Console.WriteLine($"Əvvəlki Bellek: {beforeMemory / 1024} KB");

        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();

        long afterMemory = GC.GetTotalMemory(false);
        Console.WriteLine($"Sonrakı Bellek: {afterMemory / 1024} KB");
        Console.WriteLine($"Fərq: {(beforeMemory - afterMemory) / 1024} KB");
    }
}