using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Enums
{
    public enum OrderStatus
    {
        Pending = 1,         // بانتظار عروض الأسعار من الفنيين
        OfferAccepted = 2,   // تم قبول عرض فني
        InProgress = 3,      // الفني في الطريق أو يعمل على الخدمة
        Completed = 4,       // تم إنهاء الطلب بنجاح
        Cancelled = 5        // تم إلغاء الطلب
    }
}
