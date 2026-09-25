namespace ConsoleApp51.DiscountCodeSystem;

internal class DiscountService
{
    private List<DiscountCode> _discountCodes = new List<DiscountCode>();
    public void AddDiscountCode(DiscountCode discountCode)
    {
        _discountCodes.Add(discountCode);
    }
    public decimal ApplyDiscountCode(int orderId, string code, decimal orderAmount)
    {
        var discountCode = _discountCodes.FirstOrDefault(dc => dc.Code == code);
        if (discountCode == null)
        {
            throw new ArgumentException("Invalid discount code.");
        }
        if (orderAmount < discountCode.MinimumOrderAmount)
        {
            throw new ArgumentException("Order amount is below the minimum required for this discount code.");
        }
        if (!discountCode.IsActive || discountCode.ExpirationDate < DateTime.Now)
        {
            throw new ArgumentException("Discount code is not active or has expired.");
        }
        decimal discountAmount = orderAmount * (discountCode.DiscountPercentage / 100);
        decimal finalAmount = orderAmount - discountAmount;
        Console.WriteLine($"Order ID: {orderId}, Discount Code: {code}, Original Amount: {orderAmount}, Discount Amount: {discountAmount}, Final Amount: {finalAmount}");
        return finalAmount;
    }
}
