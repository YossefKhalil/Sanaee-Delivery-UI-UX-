
namespace Core.Application.Features.Provideres.Dtos
{
    public class ProviderDto
    {
        public int? ProviderId { get; set; }
        public string? NationalNumber { get; set; }
        public bool? IsAvailable { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    }
}
