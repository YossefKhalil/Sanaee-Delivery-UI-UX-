
namespace Core.Application.Features.ProviderCategories.Dtos
{
    public class GetProviderCategoryDto
    {

        // Provider Category Data
        public int ProviderCategoryId { get; set; }
        public int YearsOfExperience { get; set; }


        // User Data
        public int UserId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public float? AverageRating { get; set; }
        public int? TotalReviewCount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsUserActive { get; set; }


        // Provider Data
        public int ProviderId { get; set; }
        public string NationalNumber { get; set; } = string.Empty;
        public bool IsProviderAvailable { get; set; }


        // Category Data
        public int CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? IconUrl { get; set; }
    }
}
