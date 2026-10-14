using Core.Domain.Exceptions;
using Core.Domain.Enums;
using Core.Domain.Common;
using Domain.Events;
using Core.Domain.Events;

namespace Core.Domain.Entities
{
    public class Offer : BaseEntity
    {
        public DateTime ArrivalTime { get; set; }
        public decimal LaborCost { get; set; }
        public decimal? MaterialCost { get; set; }
        public string? Notes { get; set; }
        public OfferStatus Status { get; set; }
        public DateTime ExpiresAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }


        public int ProviderId { get; set; }
        public Provider Provider { get; set; } = null!;


        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;


        // =============================
        // (Domain Behaviors / Methods)
        // =============================

        // 1. (Factory Method)
        public static Offer Create(Order order, int providerId, decimal laborCost, decimal? materialCost, string? notes, DateTime arrivalTime)
        {
            if (laborCost <= 0)
                throw new DomainException("Offer price must be greater than zero.");

            var offer = new Offer
            {
                OrderId = order.Id,
                ProviderId = providerId,
                LaborCost = laborCost,
                MaterialCost = materialCost,
                Notes = notes,
                Status = OfferStatus.Pending,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(2),
                ArrivalTime = arrivalTime
            };

            // يمكنك إطلاق حدث لتبليغ العميل بوجود عرض جديد
            offer.AddDomainEvent(new OfferCreatedEvent(offer, order.UserId, providerId));

            return offer;
        }

        // 2. قبول العرض
        public void Accept()
        {
            if (Status != OfferStatus.Pending)
                throw new DomainException("Only pending offers can be accepted.");

            Status = OfferStatus.Accepted;
            UpdatedAt = DateTime.UtcNow;

            // إطلاق حدث لتنبيه الفني أنه تم قبول عرضه
             AddDomainEvent(new OfferAcceptedEvent(this.Id, this.OrderId, this.Order.UserId, this.ProviderId));
        }

        // 3. رفض العرض
        public void Reject()
        {
            if (Status != OfferStatus.Pending)
                throw new DomainException("Only pending offers can be rejected.");

            Status = OfferStatus.Rejected;
            UpdatedAt = DateTime.UtcNow;
        }

        // 4. إلغاء العرض بواسطة الفني (سحب العرض)
        public void Cancel()
        {
            if (Status == OfferStatus.Accepted)
                throw new DomainException("Cannot cancel an already accepted offer. You must cancel the order instead.");

            Status = OfferStatus.Cancelled;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
