using Core.Domain.Enums;
using Core.Domain.Common;
using Core.Domain.Exceptions;
using Core.Domain.Events;

namespace Core.Domain.Entities
{
    public class Message : BaseEntity
    {
        public string? Content { get; private set; }
        public MessageType Type { get; private set; }
        public string? AttachmentUrl { get; private set; }
        public DateTime SentAt { get; private set; }
        public DateTime? ReadAt { get; private set; }
        public int? DurationInSeconds { get; private set; }


        public bool IsRead => ReadAt.HasValue;
        public bool IsSystemMessage { get; private set; }


        public int SenderId { get; private set; }
        public User Sender { get; private set; } = null!;

        public int RecipientId { get; private set; }
        public User Recipient { get; private set; } = null!;


        public int OrderId { get; private set; }
        public Order Order { get; private set; } = null!;

        private Message() { }


        // 1. إنشاء رسالة نصية
        public static Message CreateText(int senderId, int RecipientId, int orderId, string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                throw new DomainException("محتوى الرسالة النصية لا يمكن أن يكون فارغاً.");

            var message = new Message
            {
                SenderId = senderId,
                RecipientId = RecipientId,
                OrderId = orderId,
                Type = MessageType.Text,
                Content = content.Trim(),
                SentAt = DateTime.UtcNow
            };

            message.AddDomainEvent(new MessageSentDomainEvent(message));

            return message;
        }

        // 2. إنشاء رسالة وسائط (صورة ، مستند)
        public static Message CreateMedia(int senderId, int RecipientId, int orderId, MessageType type, string attachmentUrl, string? caption = null)
        {
            if (type == MessageType.Text || type == MessageType.Location || type == MessageType.VoiceNote)
                throw new DomainException("نوع الوسائط غير صالح لهذه الدالة.");

            if (string.IsNullOrWhiteSpace(attachmentUrl))
                throw new DomainException("رابط المرفق مطلوب لرسائل الوسائط.");

            var message = new Message
            {
                SenderId = senderId,
                RecipientId = RecipientId,
                OrderId = orderId,
                Type = type,
                AttachmentUrl = attachmentUrl,
                Content = caption?.Trim(), // Optional caption
                SentAt = DateTime.UtcNow
            };

            message.AddDomainEvent(new MessageSentDomainEvent(message));

            return message;
        }

        // 3. إنشاء رسالة موقع جغرافي
        public static Message CreateLocation(int senderId, int RecipientId, int orderId, double latitude, double longitude)
        {
            var message = new Message
            {
                SenderId = senderId,
                RecipientId = RecipientId,
                OrderId = orderId,
                Type = MessageType.Location,
                Content = $"{latitude},{longitude}",
                SentAt = DateTime.UtcNow
            };

            message.AddDomainEvent(new MessageSentDomainEvent(message));

            return message;
        }


        // Factory Method متخصصة للرسائل الصوتية
        public static Message CreateVoiceNote(
            int senderId,
            int RecipientId,
            int orderId,
            string attachmentUrl,
            int durationInSeconds)
        {
            if (string.IsNullOrWhiteSpace(attachmentUrl))
                throw new DomainException("رابط التسجيل الصوتي مطلوب.");

            if (durationInSeconds <= 0)
                throw new DomainException("مدة التسجيل الصوتي يجب أن تكون أكبر من صفر.");

            var message = new Message
            {
                SenderId = senderId,
                RecipientId = RecipientId,
                OrderId = orderId,
                Type = MessageType.VoiceNote,
                AttachmentUrl = attachmentUrl,
                DurationInSeconds = durationInSeconds,
                SentAt = DateTime.UtcNow
            };

            message.AddDomainEvent(new MessageSentDomainEvent(message));

            return message;
        }

        // دالة تحديث حالة القراءة
        public void MarkAsRead()
        {
            if (ReadAt is null)
                ReadAt = DateTime.UtcNow;
        }

    }
}
