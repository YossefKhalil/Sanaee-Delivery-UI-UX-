

using Core.Domain.Enums;

namespace Core.Domain.Entities
{
    public class AuditLog
    {
        public int LogId { get; set; }
        public EntityName EntityName { get; set; }
        public int EntityId { get; set; }
        public ActionType Action { get; set; }
        public string Reason { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;


        // الشخص الذي قام بالإجراء (قد يكون الأدمن أو العميل نفسه)
        public int PerformedById { get; set; }
        public User? PerformedBy { get; set; }

        // تمييز هل الإجراء ذابي (Self-Service) أم من الإدارة
        public bool IsSelfAction { get; set; }

        // معلومات أمنية إضافية
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }

    }
}
