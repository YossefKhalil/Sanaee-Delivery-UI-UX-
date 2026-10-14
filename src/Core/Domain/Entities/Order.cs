using Core.Domain.Enums;
using Core.Domain.Common;
using Domain.Events;
using Core.Domain.Exceptions;
using Core.Domain.Events;


namespace Core.Domain.Entities
{
    public class Order : BaseEntity
    {
        
        public DateTime? VisitingTime { get; set; }
        public string? ProblemPhoto { get; set; }
        public string ProblemDescription { get; set; } = string.Empty;
        public OrderStatus Status { get;  set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsClientReviewed { get; set; } = false;
        public bool IsProviderReviewed { get; set; } = false;

        public int UserId { get; set; }
        public User User { get; set; } = null!;


        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;


        public int AddressId { get; set; }
        public Address Address { get; set; } = null!;


        public int? AcceptedOfferId { get; set; }
        public Offer? Offer { get; set; }


        public ICollection<Message> Messages { get; set; } = new List<Message>();
        public ICollection<Offer> Offers { get; set; } = new List<Offer>();
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();




        public void AcceptOffer(int offerId)
        {
            if (Status != OrderStatus.Pending)
                throw new DomainException("You can not accept offers for this order.");

            Status = OrderStatus.OfferAccepted;
        }

        public void StartWork()
        {
            if (Status != OrderStatus.OfferAccepted)
                throw new DomainException("You must accept offer for this order first.");

            Status = OrderStatus.InProgress;
        }

        public void Complete()
        {
            if (Status != OrderStatus.InProgress)
                throw new DomainException("You must start this order first.");

            Status = OrderStatus.Completed;
        }





        // Cancellation by customer
        public void CancelByCustomer(int ReasonId)
        {
            if (Status == OrderStatus.Cancelled)
                throw new DomainException("Order is already cancelled !");

            if (Status == OrderStatus.Completed)
                throw new DomainException("Order is already completed!");

            if (Status == OrderStatus.InProgress)
                throw new DomainException("Order is already in progress!");

            Status = OrderStatus.Cancelled;

            // Add event to notify the provider
            AddDomainEvent(new OrderCancelledByCustomerEvent(this.Id, this.UserId, this.Offer?.ProviderId, ReasonId));
        }



        // Cancellation by provider
        public void CancelByProvider(int ReasonId)
        {
            if (Status == OrderStatus.Cancelled)
                throw new DomainException("Order is already cancelled !");

            if (Status != OrderStatus.OfferAccepted && Status != OrderStatus.InProgress)
                throw new DomainException("Order con not be cancelled !");

            Status = OrderStatus.Cancelled;


            // Add event to reopen other offers
            AddDomainEvent(new OrderCancelledByProviderEvent(this.Id, this.UserId, this.Offer!.ProviderId, ReasonId));
        }



        // Cancellation by provider
        public void CancelByAdmin(int ReasonId, bool IssueRefund)
        {
            // 1. التحقق من قواعد الحماية الأساسية (Domain Invariants)
            if (Status == OrderStatus.Cancelled)
                throw new DomainException("Order is already cancelled !");

            // إذا كان الطلب مكتظاً وبدأت عمليات مالية، قد يُمنع الإلغاء المباشر دون تسوية
            if (Status == OrderStatus.Completed && !IssueRefund)
                throw new DomainException("Yos must define the refund issue first !");

            Status = OrderStatus.Cancelled;


            // Add event to reopen other offers
            AddDomainEvent(new OrderCancelledByAdminEvent(this.Id, this.UserId, this.Offer?.ProviderId, ReasonId, IssueRefund));
        }

    }
}
