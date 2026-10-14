using Core.Domain.Entities.Contract;


namespace Core.Domain.Entities
{
    public class Address : ISoftDeletable
    {
        public int AddressId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string? BuildingNumber { get; set; }
        public string? ApartmentNumber { get; set; }
        public string? Notes { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public bool IsDefault { get; set; }


        public int UserId { get; set; }
        public User User { get; set; } = null!;


        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
