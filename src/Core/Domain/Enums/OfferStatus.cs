using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Enums
{
    public enum OfferStatus
    {
        Pending = 1,     // العرض مقدّم وبانتظار موافقة العميل
        Accepted = 2,    // تم قبول العرض من العميل
        Rejected = 3,    // تم رفض العرض
        Expired = 4,     // انتهت المهلة الزمنية للعرض
        Cancelled = 5    // قام الفني بسحب/إلغاء العرض
    }
}
