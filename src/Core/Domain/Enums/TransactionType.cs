namespace Core.Domain.Enums
{
    public enum TransactionType
    {
        Deposit = 1,            // شحن رصيد للمحفظة (إيداع من فيزا / فوري / فودافون كاش)
        Withdrawal = 2,         // سحب رصيد من المحفظة إلى حساب بنكي أو محفظة كاش
        OrderPayment = 3,       // خصم قيمة خدمة/أوردر من محفظة العميل
        OrderEarning = 4,       // إضافة مستحقات/أرباح خدمة لمحفظة الفني
        PlatformCommission = 5, // خصم عمولة المنصة من محفظة الفني
        Refund = 6,             // استرداد مبلغ للعميل (في حالة إلغاء الطلب)
        AdminAdjustment = 7     // تعديل رصيد يدوي من الأدمن (مكافأة، تسوية، أو غرامة)
    }
}
