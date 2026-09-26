# 🛒 SHOPHUB - Alışveriş Platforması

## Tamamlandı Program.cs Dokumentasyon

Məni Program.cs fayli bütün mövcud kodlarını birləşdirərək yaradıq. İşdə olan system komprehensiv əlavə panel əvvəlcədən tətbiq olunmuş siniflər istifadə edir.

---

## 📋 MENYUNUN TƏFSİLATI

### 1️⃣ MÜŞTƏRI & MƏHSUL YÖNETİMİ

**1. Müştəri Əlavə Et** (`AddCustomer()`)
- Adı, soyadı, email və telefon nömrəsi daxil etmə
- Otomatik ID təyin
- Müştəri siyahısına əlavə

**2. Məhsul Əlavə Et** (`AddProduct()`)
- Məhsul adı, təsvir, qiymət, stok
- Kateqoriya seçimi (Electronics/Clothing)
- ProductService vasitəsilə siyahıya əlavə

**3. Bütün Məhsulları Göstər** (`ShowProducts()`)
- Aktiv məhsulları cədvəl formasında göstərə
- Stok vəziyyətini kontrol
- ProductExtensions `IsInStock()` metodundan istifadə

**4. Məhsul Axtar** (`SearchProduct()`)
- Ad əsasında məhsul axtarış
- LINQ `Where` və `Contains` istifadə

**5. Məhsulları Filtrə Et** (`FilterProducts()`)
- Kateqoriya, minimum və maksimum qiymət əsasında filtrasyon
- LINQ `Where` ilə çoxsaylı şərt

---

### 2️⃣ MƏHSUL SİLMƏ OPERASIYALARI

**13. Məhsul Sil** (`DeleteProduct()`)
- **Soft Delete** - məhsul fiziki olaraq silinmir
- `IsDeleted` flag-i true olur
- ProductService `RemoveProduct()` istifadə

**14. Məhsulu Bərpa Et** (`RestoreProduct()`)
- Silinmiş məhsulu aktiv etmə
- `IsDeleted = false`
- ProductService `RestoreProduct()` istifadə

**15. Silinmiş Məhsulları Göstər** (`ShowDeletedProducts()`)
- Yalnız silinmiş məhsulları göstər
- Silinmiş məhsul sayı göstər

---

### 3️⃣ SİFARİŞ OPERASIYALARI

**6. Sifariş Yarat** (`CreateOrder()`)
- Müştəri seçməsi
- Yeni Order yaratma
- OrderService `CreateOrder()` istifadə
- Pending status ilə başlama

**7. Sifariş-ə Məhsul Əlavə Et** (`AddProductToOrder()`)
- Aktif sifarişləri göstər
- Məhsul və miqdar seçimi
- Stok kontrol
- OrderService `AddProductToOrder()`
- Stock azaldılma

**8. Sifariş-dən Məhsul Çıxar** (`RemoveProductFromOrder()`)
- Sifarişdə olan məhsulları göstər
- Məhsulu çıxarma
- Stock geri qaytarma

**9. Sifariş Göstər** (`ShowOrder()`)
- Sifariş detaylarını göstər
- Müştəri, status, tarix, məhsullar
- Cəmi məbləğ hesablama

**10. Sifariş Təsdiqlə (Ödəniş)** (`ConfirmOrder()`)
- Ödəniş metodu seçimi:
  - 💵 Nağd Ödəniş (`CashPayment`)
  - 💳 Kart Ödənişi (`CardPayment`)
  - 🌐 Online Ödəniş (`OnlinePayment`)
- **Promo Kod Sistemi**:
  - WELCOME10 (10% endirim, min. 50₼)
  - SUMMER20 (20% endirim, min. 100₼)
  - SHOPHUB15 (15% endirim, min. 75₼)
- DiscountService `ApplyDiscountCode()` istifadə
- PaymentStatus: Pending → Paid
- OrderStatus: Pending → Confirmed

**11. Sifariş Ləğv Et** (`CancelOrder()`)
- Sifariş statusu Cancelled olur
- Məhsullar stocka geri qaytarılır

**12. Müştəri Sifarişlərini Göstər** (`ShowCustomerOrders()`)
- Müştəri seçimi
- Onun bütün sifarişləri göstər
- Status və məbləğ göstər

---

### 4️⃣ STATİSTİKA VƏ ANALİTİKA

**16. Məhsul Statistikası** (`ProductStatistics()`)

Göstərən məlumatlar:
- 📦 Ümumi Məhsul Sayı
- 🗑️ Silinmiş Məhsul Sayı
- 👥 Ümumi Müştəri Sayı
- 📋 Ümumi Sifariş Sayı
- 💰 Cəmi Gəlir (sum LINQ)
- 📈 Orta Sifariş Dəyəri (average LINQ)
- 🏆 Ən Çox Satılan Məhsul (SalesStatisticsService)
- ⭐ Ən Yüksək Rəyə Sahib Məhsullar (RatingService.GetHighestRatedProducts)
- ⭐ Ən Aşağı Rəyə Sahib Məhsullar (RatingService.GetLowestRatedProducts)

---

### 5️⃣ SİSTEM ALETLƏRI

**17. Object Inspector** (`ObjectInspector()`)
- **Reflection** istifadə edir
- Product, Customer, Order siniflərini seçə bilər
- Properties, Fields, Methods göstər
- `ObjectInspector.InspectObject()` istifadə

**18. Garbage Collection Test** (`GarbageCollectionTest()`)
```
GC.GetTotalMemory() - Əvvəlki bellek  
GC.GetAllocatedBytesForCurrentThread() - Ayrılmış bayt
GC.GetGeneration() - Nəsil
GC.Collect() - Çöp yığılması
GC.WaitForPendingFinalizers() - Gözləmə
```
MemoryTest sinfı tərəfindən tətbiq olunur.

---

## 🔧 İSTİFADƏ OLUNAN TEXNOLOJI VƏ ŞABLONLAR

### Interfaceler
- ✅ `IPaymentService` - Ödəniş interfeysi
- ✅ `IProductService` - Məhsul xidməti
- ✅ `IOrderService` - Sifariş xidməti

### Siniflər
- ✅ `Customer` - Müştəri modeli
- ✅ `Product` - Məhsul modeli (inheritance: ClothingProduct, ElectronicProduct)
- ✅ `Order` - Sifariş modeli
- ✅ `OrderItem` - Sifariş maddəsi
- ✅ `OrderService` - Sifariş idarəəsi
- ✅ `ProductService` - Məhsul idarəəsi
- ✅ `DiscountCode` - Endirim kodu
- ✅ `DiscountService` - Endirim idarəəsi
- ✅ `ProductRating` - Məhsul rəyi
- ✅ `RatingService` - Rəy idarəəsi
- ✅ `SalesStatisticsService` - Satış statistikası
- ✅ `CashPayment` - Nağd ödəniş
- ✅ `CardPayment` - Kart ödənişi
- ✅ `OnlinePayment` - Online ödəniş
- ✅ `CustomCollection<T>` - Özəl koleksiya
- ✅ `ObjectInspector` - Reflection inspektoru
- ✅ `MemoryTest` - Bellek testi

### Extensionlar
- ✅ `ProductExtensions.IsInStock()` - Stokda var mı?
- ✅ `ProductExtensions.GetFinalPrice()` - Son qiymət
- ✅ `ProductExtensions.IsExpensive()` - Bahı mı?

### Enums
- ✅ `Category` - Kateqoriya (Electronics, Clothing)
- ✅ `Status` - Sifariş Statusu (Pending, Confirmed, Shipped, Delivered, Cancelled)
- ✅ `PaymentStatus` - Ödəniş Statusu (Pending, Paid, Failed, Refunded)

### Exceptions
- ✅ `CustomerNotFoundException`
- ✅ `ProductNotFoundException`
- ✅ `OrderNotFoundException`
- ✅ `InvalidOrderException`
- ✅ `OutOfStockException`

---

## 🎯 TƏTBİQ OLUNAN C# MAVZULARı

1. ✅ **LINQ** - Where, Select, FirstOrDefault, Sum, Average, GroupBy, OrderBy
2. ✅ **Object Oriented Programming** - Inheritance, Encapsulation, Abstraction, Polymorphism
3. ✅ **Interfaces** - IPaymentService, IProductService, IOrderService
4. ✅ **Exception Handling** - try-catch, custom exceptions
5. ✅ **Collections** - List<T>, CustomCollection<T>
6. ✅ **Enums** - Category, Status, PaymentStatus
7. ✅ **Extension Methods** - ProductExtensions
8. ✅ **Reflection** - ObjectInspector, GetProperties(), GetFields(), GetMethods()
9. ✅ **Generics** - CustomCollection<T>
10. ✅ **Garbage Collection** - GC.Collect(), GC.GetTotalMemory(), GC.WaitForPendingFinalizers()
11. ✅ **Soft Delete Pattern** - IsDeleted flag
12. ✅ **Payment Strategy Pattern** - IPaymentService implementations
13. ✅ **Service Layer Pattern** - ProductService, OrderService, DiscountService, RatingService
14. ✅ **SOLID Principles** - Single Responsibility, Open/Closed, Liskov Substitution
15. ✅ **DateTime & Formatting** - DateTime.Now, Currency formatting (:C)

---

## 💾 DEFAULT DATA

Proqram başladığında öncədən doldurulmuş məlumatlar:

### Məhsullar:
1. Laptop - 1500₼ (Electronics)
2. T-Shirt - 25₼ (Clothing)
3. Phone - 800₼ (Electronics)

### Müştərilər:
1. Aykhan Hasanov - aykhan@email.com
2. Leila Yusifova - leila@email.com

### Endirim Kodları:
- WELCOME10 - 10% (minimum 50₼, 30 gün)
- SUMMER20 - 20% (minimum 100₼, 90 gün)
- SHOPHUB15 - 15% (minimum 75₼, 60 gün)

---

## 🚀 BAŞLAMA QAYDASI

```bash
cd ConsoleApp51
dotnet run
```

Bütün funksionallıq menyu vasitəsilə asanlıqla istifadə edilə bilər.

---

## ✅ TAMAMLANMIŞ TƏLƏBİ

✔️ Həm 18 menyu əməliyyatı  
✔️ Ödəniş sistemi (Cash, Card, Online)  
✔️ Promo kod sistemi (WELCOME10, SUMMER20, SHOPHUB15)  
✔️ Məhsul rating sistemi (1-5 ⭐)  
✔️ Soft delete funksionallığı  
✔️ CustomCollection<T> (List<T> olmadan)  
✔️ İstisna idarəəsi  
✔️ LINQ istifadə  
✔️ Object Inspector (Reflection)  
✔️ Garbage Collection Test  
✔️ OOP prinsipləri  
✔️ Service Layer Üzən Modeli

---

**GitHub Depohavası**: https://github.com/Aykhan003/C-Final-Project
