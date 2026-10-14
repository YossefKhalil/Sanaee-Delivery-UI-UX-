

namespace Core.Domain.Entities
{
    public class ProviderCategory
    {
        public int ProviderCategoryId { get; set; }
        public int YearsOfExperience { get; set; }


        public int ProviderId { get; set; }
        public Provider Provider { get; set; } = null!;


        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
    }
}
