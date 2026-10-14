using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Enums
{
    public enum DocumentStatus
    {
        Pending = 1,          // المستند مرفوع وبانتظار مراجعة الأدمن
        Approved = 2,         // تم فحص المستند وقبوله
        Rejected = 3,         // تم رفض المستند بالكامل
        Expired = 4,          // المستند انتهت صلاحيته الزمنيّة (مثل رخصة قيادة منتهية)
        ReUploadRequired = 5  // مقبول مبدئياً لكن يتطلب إعادة رفع (مثلاً: الصورة غير واضحة)
    }
}
