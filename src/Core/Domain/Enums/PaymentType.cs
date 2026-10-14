namespace Core.Domain.Enums
{
    public enum PaymentType
    {
        OrderPayment = 1,     // دفع مقابل طلب خدمة
        WalletTopUp = 2,      // شحن رصيد المحفظة
        SubscriptionFee = 3   // رسوم اشتراك أو عمولة منصة
    }
}
