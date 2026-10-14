namespace Core.Domain.Enums
{
    public enum PaymentStatus
    {
        Pending = 1,    // العملية معلقة / بانتظار التأكيد
        Success = 2,    // تم الدفع بنجاح
        Failed = 3,     // فشلت عملية الدفع
        Refunded = 4    // تم استرداد المبلغ
    }
}
