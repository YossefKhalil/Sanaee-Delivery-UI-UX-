using System.ComponentModel.DataAnnotations.Schema;
using Core.Domain.Entities.Contract;
using Core.Domain.Exceptions;

namespace Core.Domain.Entities
{
    public class Review : ISoftDeletable
    {
        // ثوابت قواعد العمل (Business Rule)
        private const int EditingWindowInHours = 48; // مهلة 48 ساعة مثلاً

        public int ReviewId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsEdited { get; private set; } // لمعرفة ما إذا تم تعديله سابقاً


        // 1. خاصية محسبة (Calculated Property) لا تُخزن في قاعدة البيانات
        [NotMapped]
        public bool IsEditable => DateTime.UtcNow <= CreatedAt.AddHours(EditingWindowInHours);

        public int RevieweeId { get; set; }
        public User Reviewee { get; set; } = null!;


        public int ReviewerId { get; set; }
        public User Reviewer { get; set; } = null!;


        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;


        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }



        public void Update(int newRating, string? newComment)
        {
            // القاطعة الأولى: التحقق من المهلة الزمنية
            if (!IsEditable)
                throw new DomainException("فترة تعديل التقييم قد انتهت (المسموح به 48 ساعة فقط).");

            // القاطعة الثانية: السماح بالتعديل لمرة واحدة فقط (إذا أردت تطبيقها)
            if (IsEdited)
                throw new DomainException("تم تعديل هذا التقييم من قبل، لا يمكن تعديله مرة أخرى.");

            // التعديل
            Rating = newRating;
            Comment = newComment;
            UpdatedAt = DateTime.UtcNow;
            IsEdited = true;
        }
    }
}
