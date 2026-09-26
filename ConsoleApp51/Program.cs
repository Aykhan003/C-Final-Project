using ConsoleApp51.Collections;
using ConsoleApp51.DiscountCodeSystem;
using ConsoleApp51.Enum;
using ConsoleApp51.Exceptions;
using ConsoleApp51.Extensions;
using ConsoleApp51.Models;
using ConsoleApp51.PaymentMethods;
using ConsoleApp51.ProductRatingSystem;
using ConsoleApp51.Reflection;
using ConsoleApp51.SalesStaticsSystem;
using ConsoleApp51.Services;
using ConsoleApp51.Tests;
using MemoryTest = ConsoleApp51.Tests.MemoryTest;

namespace ConsoleApp51;

class Program
{
    private static List<Customer> _customers = new();
    private static List<Product> _products = new();
    private static List<Order> _orders = new();
    private static ProductService _productService = new();
    private static OrderService _orderService = new();
    private static DiscountService _discountService = new();
    private static RatingService _ratingService;
    private static SalesStatisticsService _statisticsService;
    private static int _productIdCounter = 1;
    private static int _customerIdCounter = 1;
    private static int _orderId = 1;

    static void Main()
    {
        InitializeDefaultData();
        _ratingService = new RatingService(_orders);
        _statisticsService = new SalesStatisticsService(_orders);

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
                        Console.WriteLine("\n👋 Proqramdan çıxırız...");
                        return;
                    default:
                        Console.WriteLine("❌ Yanlış seçim! Lütfen təkrar edin.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Xəta yarandı: {ex.Message}");
            }

            Console.WriteLine("\nDevam etmək üçün Enter tuşuna basın...");
            Console.ReadLine();
            Console.Clear();
        }
    }

    private static void InitializeDefaultData()
    {
        // Default discount codes
        _discountService.AddDiscountCode(new DiscountCode("WELCOME10", 10, DateTime.Now.AddDays(30), true, 50));
        _discountService.AddDiscountCode(new DiscountCode("SUMMER20", 20, DateTime.Now.AddDays(90), true, 100));
        _discountService.AddDiscountCode(new DiscountCode("SHOPHUB15", 15, DateTime.Now.AddDays(60), true, 75));

        // Default products
        var product1 = new Product(1, "Laptop", "High-performance laptop", 1500m, 10, Category.Electronics);
        var product2 = new Product(2, "T-Shirt", "Comfortable cotton t-shirt", 25m, 50, Category.Clothing);
        var product3 = new Product(3, "Phone", "Latest smartphone", 800m, 15, Category.Electronics);

        _products.Add(product1);
        _products.Add(product2);
        _products.Add(product3);

        _productService.AddProduct(product1);
        _productService.AddProduct(product2);
        _productService.AddProduct(product3);

        _productIdCounter = 4;

        // Default customers
        var customer1 = new Customer(1, "Aykhan", "Hasanov", "aykhan@email.com", "+994501234567");
        var customer2 = new Customer(2, "Leila", "Yusifova", "leila@email.com", "+994502234567");

        _customers.Add(customer1);
        _customers.Add(customer2);
        _customerIdCounter = 3;
    }

    private static void DisplayMainMenu()
    {
        Console.Clear();
        Console.WriteLine("\n════════════════════════════════════════════════════════════");
        Console.WriteLine("                    🛒 SHOPHUB - Alışveriş Platforması");
        Console.WriteLine("════════════════════════════════════════════════════════════\n");

        Console.WriteLine("👥 Müştəri Yönetimi:");
        Console.WriteLine("   1. Müştəri əlavə et");
        Console.WriteLine("   2. Məhsul əlavə et");

        Console.WriteLine("\n🛍️ Məhsul Yönetimi:");
        Console.WriteLine("   3. Bütün məhsulları göstər");
        Console.WriteLine("   4. Məhsul axtar");
        Console.WriteLine("   5. Məhsulları filtrə et");
        Console.WriteLine("   13. Məhsulu sil");
        Console.WriteLine("   14. Məhsulu bərpa et");
        Console.WriteLine("   15. Silinmiş məhsulları göstər");

        Console.WriteLine("\n📦 Sifariş Yönetimi:");
        Console.WriteLine("   6. Sifariş yarat");
        Console.WriteLine("   7. Sifariş-ə məhsul əlavə et");
        Console.WriteLine("   8. Sifariş-dən məhsul çıxar");
        Console.WriteLine("   9. Sifariş-i göstər");
        Console.WriteLine("   10. Sifariş-i təsdiqlə (Ödəniş)");
        Console.WriteLine("   11. Sifariş-i ləğv et");
        Console.WriteLine("   12. Müştəri sifarişlərini göstər");

        Console.WriteLine("\n📊 Analitika və Sistem:");
        Console.WriteLine("   16. Məhsul Statistikası");
        Console.WriteLine("   17. Object Inspector");
        Console.WriteLine("   18. Garbage Collection Test");

        Console.WriteLine("\n   0. Çıxış");
        Console.WriteLine("════════════════════════════════════════════════════════════\n");
        Console.Write("Seçim edin: ");
    }

    #region Customer Operations

    private static void AddCustomer()
    {
        Console.Clear();
        Console.WriteLine("👤 YENİ MÜŞTƏRI ƏLAVƏ ET");
        Console.WriteLine("════════════════════════════════════════");

        Console.Write("\nAdı: ");
        string firstName = Console.ReadLine() ?? "";

        Console.Write("Soyadı: ");
        string lastName = Console.ReadLine() ?? "";

        Console.Write("Email: ");
        string email = Console.ReadLine() ?? "";

        Console.Write("Telefon: ");
        string phone = Console.ReadLine() ?? "";

        if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Ad və soyad boş ola bilməz!");

        var customer = new Customer(_customerIdCounter++, firstName, lastName, email, phone);
        _customers.Add(customer);

        Console.WriteLine($"\n✅ Müştəri uğurla əlavə olundu!");
        Console.WriteLine($"   ID: {customer.Id}");
        Console.WriteLine($"   Ad: {customer.FirstName} {customer.LastName}");
        Console.WriteLine($"   Email: {customer.Email}");
    }

    #endregion

    #region Product Operations

    private static void AddProduct()
    {
        Console.Clear();
        Console.WriteLine("📦 YENİ MƏHSUL ƏLAVƏ ET");
        Console.WriteLine("════════════════════════════════════════");

        Console.Write("\nMəhsul adı: ");
        string name = Console.ReadLine() ?? "";

        Console.Write("Təsviri: ");
        string description = Console.ReadLine() ?? "";

        Console.Write("Qiymət: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal price) || price <= 0)
            throw new ArgumentException("Qiymət müsbət rəqəm olmalıdır!");

        Console.Write("Stok sayı: ");
        if (!int.TryParse(Console.ReadLine(), out int stock) || stock < 0)
            throw new ArgumentException("Stok sayı mənfi ola bilməz!");

        Console.WriteLine("\nKateqoriya seçin:");
        Console.WriteLine("1. Electronics");
        Console.WriteLine("2. Clothing");
        Console.Write("Seçim: ");
        string categoryChoice = Console.ReadLine() ?? "1";

        Category category = categoryChoice == "2" ? Category.Clothing : Category.Electronics;

        var product = new Product(_productIdCounter++, name, description, price, stock, category);
        _products.Add(product);
        _productService.AddProduct(product);

        Console.WriteLine($"\n✅ Məhsul uğurla əlavə olundu!");
        Console.WriteLine($"   ID: {product.Id}");
        Console.WriteLine($"   Adı: {product.Name}");
        Console.WriteLine($"   Qiymət: {product.Price:C}");
        Console.WriteLine($"   Kateqoriya: {product.Category}");
    }

    private static void ShowProducts()
    {
        Console.Clear();
        Console.WriteLine("📦 BÜTÜN MƏHSULLAR");
        Console.WriteLine("════════════════════════════════════════\n");

        var activeProducts = _products.Where(p => !p.IsDeleted).ToList();

        if (activeProducts.Count == 0)
        {
            Console.WriteLine("❌ Məhsul yoxdur!");
            return;
        }

        foreach (var product in activeProducts)
        {
            Console.WriteLine($"ID: {product.Id} | {product.Name}");
            Console.WriteLine($"   Qiymət: {product.Price:C} | Stok: {product.Stock}");
            Console.WriteLine($"   Kateqoriya: {product.Category} | Təsvir: {product.Description}");
            Console.WriteLine($"   Stokda: {(product.IsInStock() ? "✅ Mövcud" : "❌ Bitib")}");
            Console.WriteLine("────────────────────────────────────────");
        }

        Console.WriteLine($"\n📊 Cəmi Məhsul: {activeProducts.Count}");
    }

    private static void SearchProduct()
    {
        Console.Clear();
        Console.WriteLine("🔍 MƏHSUL AXTAR");
        Console.WriteLine("════════════════════════════════════════");

        Console.Write("\nAxtarış sorğusu (ad): ");
        string query = Console.ReadLine() ?? "";

        var results = _products
            .Where(p => !p.IsDeleted && p.Name.Contains(query, StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (results.Count == 0)
        {
            Console.WriteLine("\n❌ Məhsul tapılmadı!");
            return;
        }

        Console.WriteLine($"\n✅ {results.Count} məhsul tapıldı:\n");
        foreach (var product in results)
        {
            Console.WriteLine($"• {product.Name} ({product.Price:C}) - Stok: {product.Stock}");
        }
    }

    private static void FilterProducts()
    {
        Console.Clear();
        Console.WriteLine("⚙️ MƏHSULLAR FİLTRELƏ ET");
        Console.WriteLine("════════════════════════════════════════");

        Console.WriteLine("\nKateqoriya seçin:");
        Console.WriteLine("1. Electronics");
        Console.WriteLine("2. Clothing");
        Console.Write("Seçim: ");
        string categoryChoice = Console.ReadLine() ?? "1";

        Category category = categoryChoice == "2" ? Category.Clothing : Category.Electronics;

        Console.Write("\nMin qiymət: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal minPrice))
            minPrice = 0;

        Console.Write("Max qiymət: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal maxPrice))
            maxPrice = decimal.MaxValue;

        var filtered = _products
            .Where(p => !p.IsDeleted && 
                        p.Category == category && 
                        p.Price >= minPrice && 
                        p.Price <= maxPrice)
            .ToList();

        if (filtered.Count == 0)
        {
            Console.WriteLine("\n❌ Filtrlənmiş məhsul yoxdur!");
            return;
        }

        Console.WriteLine($"\n✅ {filtered.Count} məhsul tapıldı:\n");
        foreach (var product in filtered)
        {
            Console.WriteLine($"• {product.Name} ({product.Price:C}) - {product.Category}");
        }
    }

    private static void DeleteProduct()
    {
        Console.Clear();
        Console.WriteLine("🗑️ MƏHSUL SİL (Soft Delete)");
        Console.WriteLine("════════════════════════════════════════");

        Console.Write("\nMəhsul ID-ni daxil edin: ");
        if (!int.TryParse(Console.ReadLine(), out int productId))
            throw new ArgumentException("Yanlış ID!");

        var product = _products.FirstOrDefault(p => p.Id == productId);
        if (product == null)
            throw new ProductNotFoundException($"ID {productId} olan məhsul tapılmadı!");

        _productService.RemoveProduct(productId);
        Console.WriteLine($"\n✅ Məhsul '{product.Name}' soft delete ilə silindi!");
    }

    private static void RestoreProduct()
    {
        Console.Clear();
        Console.WriteLine("♻️ MƏHSUL BƏRPA ET");
        Console.WriteLine("════════════════════════════════════════");

        Console.Write("\nMəhsul ID-ni daxil edin: ");
        if (!int.TryParse(Console.ReadLine(), out int productId))
            throw new ArgumentException("Yanlış ID!");

        var product = _products.FirstOrDefault(p => p.Id == productId);
        if (product == null)
            throw new ProductNotFoundException($"ID {productId} olan məhsul tapılmadı!");

        _productService.RestoreProduct(productId);
        Console.WriteLine($"\n✅ Məhsul '{product.Name}' bərpa olundu!");
    }

    private static void ShowDeletedProducts()
    {
        Console.Clear();
        Console.WriteLine("🗑️ SİLİNMİŞ MƏHSULLAR");
        Console.WriteLine("════════════════════════════════════════\n");

        var deletedProducts = _products.Where(p => p.IsDeleted).ToList();

        if (deletedProducts.Count == 0)
        {
            Console.WriteLine("❌ Silinmiş məhsul yoxdur!");
            return;
        }

        foreach (var product in deletedProducts)
        {
            Console.WriteLine($"ID: {product.Id} | {product.Name} ({product.Price:C})");
        }

        Console.WriteLine($"\n📊 Cəmi Silinmiş Məhsul: {deletedProducts.Count}");
    }

    #endregion

    #region Order Operations

    private static void CreateOrder()
    {
        Console.Clear();
        Console.WriteLine("🆕 SİFARİŞ YARAT");
        Console.WriteLine("════════════════════════════════════════");

        if (_customers.Count == 0)
        {
            Console.WriteLine("❌ Əvvəl müştəri əlavə edin!");
            return;
        }

        Console.WriteLine("\nMüştərilər:");
        foreach (var cust in _customers)
        {
            Console.WriteLine($"{cust.Id}. {cust.FirstName} {cust.LastName}");
        }

        Console.Write("\nMüştəri ID seçin: ");
        if (!int.TryParse(Console.ReadLine(), out int customerId))
            throw new ArgumentException("Yanlış müştəri ID!");

        var selectedCustomer = _customers.FirstOrDefault(c => c.Id == customerId);
        if (selectedCustomer == null)
            throw new CustomerNotFoundException($"Müştəri tapılmadı!");

        var order = _orderService.CreateOrder(selectedCustomer);
        _orders.Add(order);

        Console.WriteLine($"\n✅ Sifariş yağışarsa yaradıldı!");
        Console.WriteLine($"   Sifariş ID: {order.Id}");
        Console.WriteLine($"   Müştəri: {selectedCustomer.FirstName} {selectedCustomer.LastName}");
    }

    private static void AddProductToOrder()
    {
        Console.Clear();
        Console.WriteLine("➕ MƏHSULU SİFARİŞ-Ə ƏLAVƏ ET");
        Console.WriteLine("════════════════════════════════════════");

        if (_orders.Count == 0)
        {
            Console.WriteLine("❌ Əvvəl sifariş yaratın!");
            return;
        }

        Console.WriteLine("\nSifariş seç:");
        var pendingOrders = _orders.Where(o => !o.IsDeleted && o.Status == Status.Pending).ToList();

        foreach (var order in pendingOrders)
        {
            Console.WriteLine($"Sifariş ID: {order.Id} - Müştəri: {order.Customer.FirstName} {order.Customer.LastName}");
        }

        Console.Write("\nSifariş ID: ");
        if (!int.TryParse(Console.ReadLine(), out int orderId))
            throw new ArgumentException("Yanlış sifariş ID!");

        var order_ = _orders.FirstOrDefault(o => o.Id == orderId);
        if (order_ == null || order_.Status != Status.Pending)
            throw new OrderNotFoundException("Sifariş tapılmadı!");

        ShowProducts();

        Console.Write("\nMəhsul ID seçin: ");
        if (!int.TryParse(Console.ReadLine(), out int productId))
            throw new ArgumentException("Yanlış məhsul ID!");

        var product = _products.FirstOrDefault(p => p.Id == productId && !p.IsDeleted);
        if (product == null)
            throw new ProductNotFoundException("Məhsul tapılmadı!");

        if (!product.IsInStock())
            throw new OutOfStockException("Məhsul stokda bitib!");

        Console.Write("Miqdar: ");
        if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity <= 0)
            throw new ArgumentException("Miqdar 0-dan böyük olmalıdır!");

        if (quantity > product.Stock)
            throw new OutOfStockException("Stokda kifayət qədər məhsul yoxdur!");

        _orderService.AddProductToOrder(orderId, product, quantity);
        product.Stock -= quantity;

        Console.WriteLine($"\n✅ '{product.Name}' (x{quantity}) sifariş-ə əlavə olundu!");
        Console.WriteLine($"   Məhsul Qiyməti: {product.Price:C}");
        Console.WriteLine($"   Cəmi: {product.Price * quantity:C}");
    }

    private static void RemoveProductFromOrder()
    {
        Console.Clear();
        Console.WriteLine("➖ MƏHSULU SİFARİŞ-DƏN ÇIXAR");
        Console.WriteLine("════════════════════════════════════════");

        if (_orders.Count == 0)
        {
            Console.WriteLine("❌ Sifariş yoxdur!");
            return;
        }

        Console.Write("\nSifariş ID: ");
        if (!int.TryParse(Console.ReadLine(), out int orderId))
            throw new ArgumentException("Yanlış sifariş ID!");

        var order = _orders.FirstOrDefault(o => o.Id == orderId);
        if (order == null)
            throw new OrderNotFoundException("Sifariş tapılmadı!");

        if (order.Products.Count == 0)
        {
            Console.WriteLine("❌ Sifarişdə məhsul yoxdur!");
            return;
        }

        Console.WriteLine("\nSifarişdə olan məhsullar:");
        foreach (var product in order.Products)
        {
            Console.WriteLine($"{product.Id}. {product.Name} ({product.Price:C})");
        }

        Console.Write("\nMəhsul ID: ");
        if (!int.TryParse(Console.ReadLine(), out int productId))
            throw new ArgumentException("Yanlış məhsul ID!");

        var product_ = order.Products.FirstOrDefault(p => p.Id == productId);
        if (product_ == null)
            throw new ProductNotFoundException("Məhsul sifarişdə tapılmadı!");

        _orderService.RemoveProductFromOrder(orderId, product_);
        product_.Stock += 1; // Stock-u geri qaytarın

        Console.WriteLine($"\n✅ '{product_.Name}' sifarişdən çıxarıldı!");
    }

    private static void ShowOrder()
    {
        Console.Clear();
        Console.WriteLine("📋 SİFARİŞ GÖSTƏR");
        Console.WriteLine("════════════════════════════════════════");

        Console.Write("\nSifariş ID: ");
        if (!int.TryParse(Console.ReadLine(), out int orderId))
            throw new ArgumentException("Yanlış sifariş ID!");

        var order = _orders.FirstOrDefault(o => o.Id == orderId);
        if (order == null)
            throw new OrderNotFoundException("Sifariş tapılmadı!");

        Console.WriteLine($"\n📦 Sifariş #{order.Id}");
        Console.WriteLine($"   Müştəri: {order.Customer.FirstName} {order.Customer.LastName}");
        Console.WriteLine($"   Status: {order.Status}");
        Console.WriteLine($"   Sifariş Tarixi: {order.CreatedAt:dd.MM.yyyy HH:mm}");
        Console.WriteLine("────────────────────────────────────────");

        if (order.Products.Count == 0)
        {
            Console.WriteLine("❌ Sifarişdə məhsul yoxdur!");
            return;
        }

        Console.WriteLine("\n📝 Məhsullar:");
        foreach (var product in order.Products)
        {
            Console.WriteLine($"  • {product.Name}");
            Console.WriteLine($"    Qiymət: {product.Price:C}");
            Console.WriteLine($"    Kateqoriya: {product.Category}");
        }

        Console.WriteLine("────────────────────────────────────────");
        Console.WriteLine($"💰 Cəmi: {order.TotalPrice:C}");
    }

    private static void ConfirmOrder()
    {
        Console.Clear();
        Console.WriteLine("✅ SİFARİŞ TƏSDİQLƏ (Ödəniş)");
        Console.WriteLine("════════════════════════════════════════");

        Console.Write("\nSifariş ID: ");
        if (!int.TryParse(Console.ReadLine(), out int orderId))
            throw new ArgumentException("Yanlış sifariş ID!");

        var order = _orders.FirstOrDefault(o => o.Id == orderId);
        if (order == null)
            throw new InvalidOrderException("Sifariş tapılmadı!");

        if (order.Products.Count == 0)
            throw new InvalidOrderException("Sifarişdə məhsul yoxdur!");

        Console.WriteLine($"\n💰 Sifariş Məbləği: {order.TotalPrice:C}");

        Console.WriteLine("\n💳 Ödəniş Metodunu Seçin:");
        Console.WriteLine("1. Nağd Ödəniş");
        Console.WriteLine("2. Kart Ödənişi");
        Console.WriteLine("3. Online Ödəniş");
        Console.Write("Seçim: ");
        string paymentMethod = Console.ReadLine() ?? "1";

        IPaymentService payment = paymentMethod switch
        {
            "2" => new CardPayment(),
            "3" => new OnlinePayment(),
            _ => new CashPayment()
        };

        decimal finalAmount = order.TotalPrice;

        Console.Write("\nPromo kod var mı? (Yoxsa sadəcə Enter basın): ");
        string promoCode = Console.ReadLine() ?? "";

        if (!string.IsNullOrWhiteSpace(promoCode))
        {
            try
            {
                finalAmount = _discountService.ApplyDiscountCode(orderId, promoCode, order.TotalPrice);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"⚠️ {ex.Message}");
            }
        }

        Console.WriteLine($"\n💳 Ödəniş işlənir...");
        payment.Pay(orderId, finalAmount);

        var paymentStatus = payment.GetPaymentStatus(orderId);
        if (paymentStatus == PaymentStatus.Paid)
        {
            _orderService.ConfirmOrder(orderId);
            Console.WriteLine($"\n✅ Sifariş uğurla təsdiqləndi!");
            Console.WriteLine($"   Final Məbləğ: {finalAmount:C}");
            Console.WriteLine($"   Ödəniş Status: {paymentStatus}");
        }
        else
        {
            throw new InvalidOperationException("Ödəniş uğursuz oldu!");
        }
    }

    private static void CancelOrder()
    {
        Console.Clear();
        Console.WriteLine("❌ SİFARİŞ LƏĞV ET");
        Console.WriteLine("════════════════════════════════════════");

        Console.Write("\nSifariş ID: ");
        if (!int.TryParse(Console.ReadLine(), out int orderId))
            throw new ArgumentException("Yanlış sifariş ID!");

        var order = _orders.FirstOrDefault(o => o.Id == orderId);
        if (order == null)
            throw new OrderNotFoundException("Sifariş tapılmadı!");

        _orderService.CancelOrder(orderId);

        foreach (var product in order.Products)
        {
            product.Stock += 1;
        }

        Console.WriteLine($"\n✅ Sifariş #{orderId} ləğv olundu!");
    }

    private static void ShowCustomerOrders()
    {
        Console.Clear();
        Console.WriteLine("👤 MÜŞTƏRİ SİFARİŞLƏRİ");
        Console.WriteLine("════════════════════════════════════════");

        if (_customers.Count == 0)
        {
            Console.WriteLine("❌ Müştəri yoxdur!");
            return;
        }

        Console.WriteLine("\nMüştərilər:");
        foreach (var cust in _customers)
        {
            Console.WriteLine($"{cust.Id}. {cust.FirstName} {cust.LastName}");
        }

        Console.Write("\nMüştəri ID: ");
        if (!int.TryParse(Console.ReadLine(), out int customerId))
            throw new ArgumentException("Yanlış müştəri ID!");

        var customer_ = _customers.FirstOrDefault(c => c.Id == customerId);
        if (customer_ == null)
            throw new CustomerNotFoundException("Müştəri tapılmadı!");

        var customerOrders = _orders.Where(o => o.Customer.Id == customerId && !o.IsDeleted).ToList();

        if (customerOrders.Count == 0)
        {
            Console.WriteLine($"\n❌ {customer_.FirstName}-in sifariş yoxdur!");
            return;
        }

        Console.WriteLine($"\n✅ {customer_.FirstName} {customer_.LastName}-in sifarişləri:\n");
        foreach (var order in customerOrders)
        {
            Console.WriteLine($"Sifariş #{order.Id} - Status: {order.Status} - Məbləğ: {order.TotalPrice:C}");
        }
    }

    #endregion

    #region Statistics and Analytics

    private static void ProductStatistics()
    {
        Console.Clear();
        Console.WriteLine("📊 MƏHSUL VƏ SİFARİŞ STATİSTİKASI");
        Console.WriteLine("════════════════════════════════════════\n");

        // Ümumi statistika
        int activeProducts = _products.Count(p => !p.IsDeleted);
        int deletedProducts = _products.Count(p => p.IsDeleted);
        int totalCustomers = _customers.Count;
        int totalOrders = _orders.Count;

        Console.WriteLine($"📦 Ümumi Məhsul: {activeProducts}");
        Console.WriteLine($"🗑️ Silinmiş Məhsul: {deletedProducts}");
        Console.WriteLine($"👥 Ümumi Müştəri: {totalCustomers}");
        Console.WriteLine($"📋 Ümumi Sifariş: {totalOrders}");

        if (_orders.Count > 0)
        {
            decimal totalRevenue = _orders.Sum(o => o.TotalPrice);
            decimal averageOrderValue = _statisticsService.GetAverageOrderValue();

            Console.WriteLine($"💰 Cəmi Gəlir: {totalRevenue:C}");
            Console.WriteLine($"📈 Orta Sifariş Dəyəri: {averageOrderValue:C}");
        }

        // Ən çox satılan məhsul
        if (_orders.Count > 0 && _orders.Any(o => o.Products.Count > 0))
        {
            try
            {
                var bestSelling = _statisticsService.GetBestSellingProduct();
                if (bestSelling != null)
                {
                    Console.WriteLine($"\n🏆 Ən Çox Satılan Məhsul: {bestSelling.Name} ({bestSelling.Price:C})");
                }
            }
            catch { }
        }

        // En yüksek rəy alan məhsullar
        var highestRated = _ratingService.GetHighestRatedProducts();
        if (highestRated.Count > 0)
        {
            Console.WriteLine($"\n⭐ Ən Yüksək Rəyə Sahib Məhsullar:");
            foreach (var product in highestRated.Take(5))
            {
                var avgRating = _ratingService.GetAverageRating(product);
                Console.WriteLine($"   • {product.Name}: {avgRating:F2}⭐");
            }
        }

        // En aşağı rəy alan məhsullar
        var lowestRated = _ratingService.GetLowestRatedProducts();
        if (lowestRated.Count > 0)
        {
            Console.WriteLine($"\n⭐ Ən Aşağı Rəyə Sahib Məhsullar:");
            foreach (var product in lowestRated.Take(5))
            {
                var avgRating = _ratingService.GetAverageRating(product);
                Console.WriteLine($"   • {product.Name}: {avgRating:F2}⭐");
            }
        }

        Console.WriteLine("\n════════════════════════════════════════");
    }

    #endregion

    #region System Tools

    private static void ObjectInspector()
    {
        Console.Clear();
        Console.WriteLine("🔍 OBJECT INSPECTOR (Reflection)");
        Console.WriteLine("════════════════════════════════════════");

        Console.WriteLine("\nObyekt seçin:");
        Console.WriteLine("1. Product");
        Console.WriteLine("2. Customer");
        Console.WriteLine("3. Order");
        Console.Write("Seçim: ");
        string choice = Console.ReadLine() ?? "1";

        object obj = choice switch
        {
            "2" => _customers.FirstOrDefault() ?? new Customer(1, "Test", "User", "test@mail.com", "123"),
            "3" => _orders.FirstOrDefault() ?? new Order(1, new Customer(1, "Test", "User", "test@mail.com", "123"), new List<Product>(), 0m, Status.Pending, DateTime.Now, false),
            _ => _products.FirstOrDefault() ?? new Product(1, "Test", "Product", 10m, 5, Category.Electronics)
        };

        Reflection.ObjectInspector.InspectObject(obj);
    }

    private static void GarbageCollectionTest()
    {
        Console.Clear();
        Console.WriteLine("🧹 GARBAGE COLLECTION TEST");
        Console.WriteLine("════════════════════════════════════════\n");

        MemoryTest memoryTest = new();
        memoryTest.Run();
    }

    #endregion
}
