using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Enums
{
    public enum DocumentType
    {
        NationalId = 1,        // بطاقة الرقم القومي
        CriminalRecord = 2,    // صحيفة الفيش والجنائي
        WorkPermit = 3,        // شهادة مزاولة المهنة
        PersonalPhoto = 4      // صورة شخصية رسميّة
    }
}
