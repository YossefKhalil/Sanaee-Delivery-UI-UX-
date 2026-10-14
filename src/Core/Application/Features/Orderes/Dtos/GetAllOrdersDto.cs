using Core.Domain.Enums;


namespace Core.Application.Features.Orderes.Dtos
{
    public class GetAllOrdersDto
    {
        public int OrderId { get; set; }
        public OrderStatus Status { get; private set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsClientReviewed { get; set; } = false;
        public bool IsProviderReviewed { get; set; } = false;


        public int UserId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;


        public int CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;


        public int AddressId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;


        public int? AcceptedOfferId { get; set; }
        public int? ProviderId { get; set; }
    }
}
