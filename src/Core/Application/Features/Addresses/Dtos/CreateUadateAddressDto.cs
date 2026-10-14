

namespace Core.Application.Features.Addresses.Dtos
{
    public class CreateUadateAddressDto
    {
        public string? Title { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? Street { get; set; }
        public string? BuildingNumber { get; set; }
        public string? ApartmentNumber { get; set; }
        public string? Notes { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public bool? IsDefault { get; set; }
    }
}
