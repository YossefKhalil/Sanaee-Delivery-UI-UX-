namespace Core.Application.Features.Addresses.Dtos
{
    public class AddressDto
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
    }
}
