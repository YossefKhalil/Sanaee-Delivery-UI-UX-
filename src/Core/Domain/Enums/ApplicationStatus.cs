

namespace Core.Domain.Enums
{
    public enum ApplicationStatus
    {
        Pending = 1,          // طلب الانضمام قيد المراجعة
        Approved = 2,         // تم قبول طلب انضمام الفني
        Rejected = 3,         // تم رفض الطلب
        ActionRequired = 4    // يتطلب إجراء من الفني (مثلاً: إعادة رفع الصورة)
    }
}
